﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFUtilities.Commands;
using WPFUtilities.Extensions;

namespace WPFUtilities
{
    /// <summary>
    /// The view model base class
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Dictionary to contain command bindings created by the GetCommand function
        /// </summary>
        private Dictionary<string, RelayCommand> m_commands = new Dictionary<string, RelayCommand>();

        /// <summary>
        /// Dictionary to contain properties generated with SetProperty&lt;T&gt;.
        /// </summary>
        protected Dictionary<string, object> m_properties = new Dictionary<string, object>();

        /// <summary>
        /// A set of user-supplied objects that are storage for the view model properties
        /// </summary>
        protected HashSet<object> m_propertyModels = new HashSet<object>();

        /// <summary>
        /// Initializes a new BaseVM
        /// </summary>
        public BaseVM()
        {
        }

        /// <summary>
        /// Initializes a new BaseVM that registers provided model objects
        /// </summary>
        /// <param name="models">data models to register and use as property backers</param>
        public BaseVM(params object[] models)
        {
            foreach (var m in models)
                this.RegisterModel(m);
        }

        #region Property Change Methods
        /// <summary>
        /// Provides a convenient way to send notifications.  When called inside a property,
        /// it can be called safely with the default paramater
        /// </summary>
        /// <param name="property">The name of the property that is changing. This defaults to [CallerMemberName]</param>
        protected void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            this.PropertyChanged.SafeInvoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        /// <summary>
        /// Adds the given model to the list of objects to use as a backing for the view model properties
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected void RegisterModel(object model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            // first check that this model has not been added already
            if (m_propertyModels.Contains(model))
                throw new ArgumentException("Model has already been registered");
            // next check that the model does not conflic with any of the models already added
            var modProps = model.GetType().GetProperties();
            var curProps = m_propertyModels.SelectMany(o => o.GetType().GetProperties());
            foreach (var p in modProps)
            {
                if (curProps.Where(o => o.PropertyType == p.PropertyType && o.Name == p.Name).Any())
                    throw new ArgumentException(model.ToString() + " has properties that conflict with existing registered models");
            }

            m_propertyModels.Add(model);
        }

        #region Property Setters
        /// <summary>
        /// Sets the apropriate property either on a registered model or internally.  
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="propertyName">The name of the property to set.  Supplied by CallerMemberName</param>
        protected void SetProperty<T>(T newValue, [CallerMemberName] string propertyName = "")
        {
            this.SetProperty(newValue, true, (x => { }), (x => { }), (x => true), propertyName);
        }

        /// <summary>
        /// Sets the apropriate property either on a registered model or internally.  
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="allowNull">Whether or not this property can be set to null</param>
        /// <param name="propertyName">The name of the property to set.  Supplied by CallerMemberName</param>
        protected void SetProperty<T>(T newValue, bool allowNull, [CallerMemberName] string propertyName = "")
        {
            this.SetProperty(newValue, allowNull, (x => { }), (x => { }), (x => true), propertyName);
        }

        /// <summary>
        /// Sets the apropriate property either on a registered model or internally.  
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="allowNull">Whether or not this property can be set to null</param>
        /// <param name="preceeding">Action to take before setting the property.  Recieves the new value as a parameter</param>
        /// <param name="propertyName">The name of the property to set.  Supplied by CallerMemberName</param>
        protected void SetProperty<T>(T newValue, bool allowNull, Action<T> preceeding, [CallerMemberName] string propertyName = "")
        {
            this.SetProperty(newValue, allowNull, preceeding, (x => { }), (x => true), propertyName);
        }

        /// <summary>
        /// Sets the apropriate property either on a registered model or internally.  
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="allowNull">Whether or not this property can be set to null</param>
        /// <param name="preceeding">Action to take before setting the property.  Recieves the new value as a parameter</param>
        /// <param name="following">Action to take after setting the property.  Recieves the old value as a parameter</param>
        /// <param name="propertyName">The name of the property to set.  Supplied by CallerMemberName</param>
        protected void SetProperty<T>(T newValue, bool allowNull, Action<T> preceeding, Action<T> following, [CallerMemberName] string propertyName = "")
        {
            this.SetProperty(newValue, allowNull, preceeding, following, (x => true), propertyName);
        }

        /// <summary>
        /// Sets the apropriate property either on a registered model or internally.  
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="allowNull">Whether or not this property can be set to null</param>
        /// <param name="preceeding">Action to take before setting the property.  Recieves the new value as a parameter</param>
        /// <param name="following">Action to take after setting the property.  Recieves the old value as a parameter</param>
        /// <param name="validator">Predicate that determines whether the given new value is valid.  
        /// If not, the property is not set, however the preceeding action is still invoked</param>
        /// <param name="propertyName">The name of the property to set.  Supplied by CallerMemberName</param>
        protected void SetProperty<T>(T newValue, bool allowNull, Action<T> preceeding, Action<T> following, Predicate<T> validator, [CallerMemberName] string propertyName = "")
        {
            // whether or not we changed the property's value
            bool changed = false;
            // used to simplify the control structure
            Action propertySetter;
            // the old value of the property
            T oldVal;

            // determine what our target is
            // properties could be backed by this base class or by a registered model
            var models = m_propertyModels.Where(o => o.GetType().GetProperties()
                .Where(p => p.Name == propertyName && p.PropertyType == typeof(T)).Any());
            if (models.Any())
            {
                // single garaunteed by our RegisterModel method
                changed = this.GetModelPropertySetter(newValue, models.Single(),
                    propertyName, out propertySetter, out oldVal);
            }
            // we don't have a model backing this property, find it in our properties dict
            else
            {
                changed = this.GetInternalPropertySetter(newValue, propertyName,
                    out propertySetter, out oldVal);
            }

            if (changed)
            {
                // check if new value is valid
                if (newValue == null && !allowNull)
                    return;
                if (validator != null && !validator(newValue))
                    return;
                // invoke our actions in the appropriate order
                preceeding.SafeInvoke(newValue);
                propertySetter();
                this.NotifyPropertyChanged(propertyName);
                following.SafeInvoke(oldVal);
            }
        }

        /// <summary>
        /// Trys to get an action that will change the given property to the new value when invoked
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="model">The model to find the property on</param>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="propertySetter">The result action that performs the appropriate setting. Will never be null</param>
        /// <param name="oldVal">The old value of the property</param>
        /// <returns>True if invoking propertySetter would change the value of the property</returns>
        private bool GetModelPropertySetter<T>(T newValue, object model, string propertyName, out Action propertySetter, out T oldVal)
        {
            // default values
            propertySetter = (() => { });
            oldVal = default(T);

            // retrieve the property and value by name
            var prop = model.GetType().GetProperty(propertyName);
            if (prop == null)
                throw new InvalidOperationException("Could not retrieve expected model property");
            oldVal = (T)prop.GetValue(model);
            if (oldVal != null)
            {
                // prefer using the comparable interface
                var compVal = oldVal as IComparable<T>;
                if (compVal != null)
                {
                    if (compVal.CompareTo(newValue) != 0)
                    {
                        propertySetter = (() => prop.SetValue(model, newValue));
                        return true;
                    }
                }
                // fall back on equals implementation
                else if (!oldVal.Equals(newValue))
                {
                    propertySetter = (() => prop.SetValue(model, newValue));
                    return true;
                }
            }
            else
            {
                propertySetter = (() => prop.SetValue(model, newValue));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Trys to get an action that will change the given property to the the new value when invoked. 
        /// Creates the property in the properties collection if it does not already exist
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="newValue">The value to set the property too</param>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="propertySetter">The result action that performs the appropriate setting</param>
        /// <param name="oldVal">The old value of the property</param>
        /// <returns>True if invoking propertySetter would change the value of the property</returns>
        private bool GetInternalPropertySetter<T>(T newValue, string propertyName, out Action propertySetter, out T oldVal)
        {
            propertySetter = (() => { });
            oldVal = default(T);

            object res;
            if (m_properties.TryGetValue(propertyName, out res))
            {
                oldVal = (T)res;
                if (oldVal == null)
                {
                    propertySetter = (() => m_properties[propertyName] = newValue);
                    return true;
                }
                else
                {
                    // prefer IComparable
                    var compRes = res as IComparable<T>;
                    if (compRes != null)
                    {
                        if (compRes.CompareTo(newValue) != 0)
                        {
                            propertySetter = (() => m_properties[propertyName] = newValue);
                            return true;
                        }
                    }
                    // fall back on objet equality
                    else if (!res.Equals(newValue))
                    {
                        propertySetter = (() => m_properties[propertyName] = newValue);
                        return true;
                    }
                }
            }
            // create the property if it doesn't already exist
            else
            {
                propertySetter = (() => m_properties[propertyName] = newValue);
                return true;
            }

            return false;
        }

        #endregion /* Property Setters */

        #region Property Getters
        /// <summary>
        /// Gets the value associated with this property.
        /// </summary>
        /// <typeparam name="T">The type of the property to get</typeparam>
        /// <param name="propertyName">The name of the property to retrieve.  Supplied by CallerMemberName</param>
        /// <returns>The value of the given property</returns>
        protected T GetProperty<T>([CallerMemberName] string propertyName = "")
        {
            return this.GetProperty<T>((() => { }), (x => { }), (x => x), propertyName);
        }

        /// <summary>
        /// Gets the value associated with this property.
        /// </summary>
        /// <typeparam name="T">The type of the property to get</typeparam>
        /// <param name="preceeding">Action to invoke before the property value is retrieved</param>
        /// <param name="propertyName">The name of the property to retrieve.  Supplied by CallerMemberName</param>
        /// <returns>The value of the given property</returns>
        protected T GetProperty<T>(Action preceeding, [CallerMemberName] string propertyName = "")
        {
            return this.GetProperty<T>(preceeding, (x => { }), (x => x), propertyName);
        }

        /// <summary>
        /// Gets the value associated with this property.
        /// </summary>
        /// <typeparam name="T">The type of the property to get</typeparam>
        /// <param name="preceeding">Action to invoke before the property value is retrieved</param>
        /// <param name="following">Action to be invoked after the property value is retrieved. Recieves the retrieved value as an argument</param>
        /// <param name="propertyName">The name of the property to retrieve.  Supplied by CallerMemberName</param>
        /// <returns>The value of the given property</returns>
        protected T GetProperty<T>(Action preceeding, Action<T> following, [CallerMemberName] string propertyName = "")
        {
            return this.GetProperty(preceeding, following, (x => x), propertyName);
        }

        /// <summary>
        /// Gets the value associated with this property.
        /// </summary>
        /// <typeparam name="T">The type of the property to get</typeparam>
        /// <param name="preceeding">Action to invoke before the property value is retrieved</param>
        /// <param name="following">Action to be invoked after the property value is retrieved. Recieves the retrieved value as an argument</param>
        /// <param name="modifier">Used to modify the returned value without actually setting the underlying property</param>
        /// <param name="propertyName">The name of the property to retrieve.  Supplied by CallerMemberName</param>
        /// <returns>The value of the given property</returns>
        protected T GetProperty<T>(Action preceeding, Action<T> following, Func<T, T> modifier, [CallerMemberName] string propertyName = "")
        {
            // func to simplify control flow
            Func<T> getValue;

            // we need to get the property, which may either exist in one of the registered models,
            // in our internal properties dictionary, or not exist at all.  If it does not exist,
            // we need to also create it, since we cannot gaurentee that SetProperty will be called prior
            var models = m_propertyModels.Where(o => o.GetType().GetProperties()
                .Where(p => p.Name == propertyName && p.PropertyType == typeof(T)).Any());
            if (models.Any())
            {
                // property exists on model, retrieve it
                var model = models.Single();
                var prop = model.GetType().GetProperty(propertyName);
                if (prop == null)
                    throw new InvalidOperationException("Could not retrieve expected model property: " + propertyName);
                getValue = (() => (T)prop.GetValue(model));
            }
            else
            {
                // property does not exist on model
                object res;
                if (!m_properties.TryGetValue(propertyName, out res))
                {
                    m_properties.Add(propertyName, default(T));
                    return default(T);
                }

                var prop = this.GetType().GetProperty(propertyName);
                if (prop == null)
                    throw new InvalidOperationException("Could not retrieve expected model property: " + propertyName);
                if (!prop.PropertyType.IsAssignableFrom(typeof(T)))
                    throw new ArgumentException("Property type cannot be assigned to expected type: " + typeof(T).FullName);
                getValue = (() => (T)res);
            }

            // invoke our delegates in the apropriate order
            preceeding.SafeInvoke();
            T retVal = getValue();
            following.SafeInvoke(retVal);
            retVal = modifier.SafeInvoke(retVal);
            return retVal;
        }

        #endregion

        #region Command Getters
        /// <summary>
        /// Gets the command associated with the given property and delegate.
        /// <b>For use outside of property setters and getters</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="selector">The expression that selects the appropriate property</param>
        /// <returns>The command composed of the supplied delegate</returns>
        protected ICommand GetCommand<T>(Action<object> execute, Expression<Func<T>> selector)
        {
            return this.GetCommand(execute, (x => true), ExpressionMethods.GetName(selector));
        }

        /// <summary>
        /// Gets the command associated with the given property and delegate.
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied delegate</returns>
        protected ICommand GetCommand(Action<object> execute, [CallerMemberName] string propertyName = "")
        {
            return this.GetCommand(execute, (x => true), propertyName);
        }

        /// <summary>
        /// Gets the command associated with the given property and delegate.
        /// <b>For use outside of property setters and getters</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="selector">The expression that selects the appropriate property</param>
        /// <returns>The command composed of the supplied delegate</returns>
        protected ICommand GetCommand<T>(Action execute, Expression<Func<T>> selector)
        {
            return this.GetCommand((x => execute()), (x => true), ExpressionMethods.GetName(selector));
        }

        /// <summary>
        /// Gets the command associated with the given property and delegate.
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied delegate</returns>
        protected ICommand GetCommand(Action execute, [CallerMemberName] string propertyName = "")
        {
            return this.GetCommand((x => execute()), (x => true), propertyName);//
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates.
        /// <b>For use outside of property setters and getters</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="canExecute">Func that is used to determine if the command can be executed</param>
        /// <param name="selector">The expression that selects the appropriate property</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand<T>(Action execute, Func<bool> canExecute, Expression<Func<T>> selector)
        {
            return this.GetCommand((x => execute()), (x => canExecute()), ExpressionMethods.GetName(selector));
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates.
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="canExecute">Func that is used to determine if the command can be executed</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand(Action execute, Func<bool> canExecute, [CallerMemberName] string propertyName = "")
        {
            return this.GetCommand((x => execute()), (x => canExecute()), propertyName);
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates.
        /// <b>For use outside of property setters and getters</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="canExecute">Func that is used to determine if the command can be executed</param>
        /// <param name="selector">The expression that selects the appropriate property</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand<T>(Action<object> execute, Func<bool> canExecute, Expression<Func<T>> selector)
        {
            return this.GetCommand(execute, (x => canExecute()), ExpressionMethods.GetName(selector));
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates.
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="canExecute">Func that is used to determine if the command can be executed</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand(Action<object> execute, Func<bool> canExecute, [CallerMemberName] string propertyName = "")
        {
            return this.GetCommand(execute, (x => canExecute()), propertyName);
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates.
        /// <b>For use outside of property setters and getters</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="canExecute">Predicate that is used to determine if the command can be executed</param>
        /// <param name="selector">The expression that selects the appropriate property</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand<T>(Action<object> execute, Predicate<object> canExecute, Expression<Func<T>> selector)
        {
            return this.GetCommand(execute, canExecute, ExpressionMethods.GetName(selector));
        }

        /// <summary>
        /// Gets the command associated with the given property and delegates
        /// <b>For use in property setters and getters only</b>
        /// </summary>
        /// <param name="execute">Action to be performed by the command. Recieves the command parameter as an argument</param>
        /// <param name="canExecute">Predicate that is used to determine if the command can be executed</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied delegates</returns>
        protected ICommand GetCommand(Action<object> execute, Predicate<object> canExecute, [CallerMemberName] string propertyName = "")
        {
            // find the command or create it if it doesn't exit
            RelayCommand res;
            if (!m_commands.TryGetValue(propertyName, out res))
            {
                res = new RelayCommand(execute, canExecute);
                m_commands.Add(propertyName, res);
            }

            return res;
        }
        #endregion
    }
}
