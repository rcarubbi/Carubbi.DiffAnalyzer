﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Carubbi.DiffAnalyzer {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Carubbi.DiffAnalyzer.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Added.
        /// </summary>
        internal static string DiffState_Added {
            get {
                return ResourceManager.GetString("DiffState_Added", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleted.
        /// </summary>
        internal static string DiffState_Deleted {
            get {
                return ResourceManager.GetString("DiffState_Deleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Modified.
        /// </summary>
        internal static string DiffState_Modified {
            get {
                return ResourceManager.GetString("DiffState_Modified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not Changed.
        /// </summary>
        internal static string DiffState_NotChanged {
            get {
                return ResourceManager.GetString("DiffState_NotChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknow.
        /// </summary>
        internal static string DiffState_Unknow {
            get {
                return ResourceManager.GetString("DiffState_Unknow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New Value.
        /// </summary>
        internal static string NewValueTextField {
            get {
                return ResourceManager.GetString("NewValueTextField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Old Value.
        /// </summary>
        internal static string OldValueTextField {
            get {
                return ResourceManager.GetString("OldValueTextField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property Name.
        /// </summary>
        internal static string PropertyNameTextField {
            get {
                return ResourceManager.GetString("PropertyNameTextField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Status.
        /// </summary>
        internal static string StatusTextField {
            get {
                return ResourceManager.GetString("StatusTextField", resourceCulture);
            }
        }
    }
}
