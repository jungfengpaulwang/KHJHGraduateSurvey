﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.18444
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JH_KH_GraduateSurvey.Properties {
    using System;
    
    
    /// <summary>
    ///   用於查詢當地語系化字串等的強類型資源類別。
    /// </summary>
    // 這個類別是自動產生的，是利用 StronglyTypedResourceBuilder
    // 類別透過 ResGen 或 Visual Studio 這類工具。
    // 若要加入或移除成員，請編輯您的 .ResX 檔，然後重新執行 ResGen
    // (利用 /str 選項)，或重建您的 VS 專案。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   傳回這個類別使用的快取的 ResourceManager 執行個體。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("JH_KH_GraduateSurvey.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   覆寫目前執行緒的 CurrentUICulture 屬性，對象是所有
        ///   使用這個強類型資源類別的資源查閱。
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
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap admissions_up_128 {
            get {
                object obj = ResourceManager.GetObject("admissions_up_128", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;國中畢業學生進路調查&quot;&gt;
        ///    &lt;DuplicateDetection&gt;
        ///        &lt;Detector Name=&quot;身分證號&quot;&gt;
        ///          &lt;Field Name=&quot;身分證號&quot; /&gt;
        ///        &lt;/Detector&gt;
        ///    &lt;/DuplicateDetection&gt;
        ///    &lt;FieldList&gt;  
        ///        &lt;Field Required=&quot;True&quot; Name=&quot;填報學年度&quot; Description=&quot;&quot; &gt;
        ///            &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「填報學年度」僅允許使用阿拉伯數字民國年。&quot; ErrorType=&quot;Error&quot; Validator=&quot;填報學年度允許範圍&quot; When=&quot;&quot; /&gt; 
        ///        &lt;/Field&gt;
        ///        &lt;Fiel [字串的其餘部分已遭截斷]&quot;; 的當地語系化字串。
        /// </summary>
        internal static string Approach_Import {
            get {
                return ResourceManager.GetString("Approach_Import", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap Export_Image {
            get {
                object obj = ResourceManager.GetObject("Export_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;xsl:stylesheet version=&quot;1.0&quot; xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;&gt;
        ///&lt;xsl:template match=&quot;/&quot;&gt;
        ///&lt;xsl:variable name=&quot;error&quot;&gt;Error&lt;/xsl:variable&gt;
        ///&lt;xsl:variable name=&quot;warning&quot;&gt;Warning&lt;/xsl:variable&gt;
        ///&lt;xsl:variable name=&quot;true&quot;&gt;TRUE&lt;/xsl:variable&gt;
        ///&lt;xsl:variable name=&quot;false&quot;&gt;FALSE&lt;/xsl:variable&gt;
        ///&lt;xsl:variable name=&quot;smallcase&quot; select=&quot;&apos;abcdefghijklmnopqrstuvwxyz&apos;&quot; /&gt;
        ///&lt;xsl:variable name=&quot;uppercase&quot; select=&quot;&apos;ABCDEFGHIJKLMNOPQRSTUVWXYZ&apos;&quot; /&gt;
        ///&lt;html&gt;
        ///	&lt;head&gt;
        ///	&lt;s [字串的其餘部分已遭截斷]&quot;; 的當地語系化字串。
        /// </summary>
        internal static string format {
            get {
                return ResourceManager.GetString("format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap Import_Image {
            get {
                object obj = ResourceManager.GetObject("Import_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap loading {
            get {
                object obj = ResourceManager.GetObject("loading", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap paste_64 {
            get {
                object obj = ResourceManager.GetObject("paste_64", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap school_search_128 {
            get {
                object obj = ResourceManager.GetObject("school_search_128", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;國中畢業未升學未就業學生動向&quot;&gt;
        ///    &lt;DuplicateDetection&gt;
        ///        &lt;Detector Name=&quot;身分證號&quot;&gt;
        ///          &lt;Field Name=&quot;身分證號&quot; /&gt;
        ///        &lt;/Detector&gt;
        ///    &lt;/DuplicateDetection&gt;
        ///    &lt;FieldList&gt;  
        ///        &lt;Field Required=&quot;True&quot; Name=&quot;填報學年度&quot; Description=&quot;&quot; &gt;
        ///            &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「填報學年度」僅允許使用阿拉伯數字民國年。&quot; ErrorType=&quot;Error&quot; Validator=&quot;填報學年度允許範圍&quot; When=&quot;&quot; /&gt; 
        ///        &lt;/Field&gt;
        ///        &lt; [字串的其餘部分已遭截斷]&quot;; 的當地語系化字串。
        /// </summary>
        internal static string Vagrant_Import {
            get {
                return ResourceManager.GetString("Vagrant_Import", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Drawing.Bitmap 的當地語系化資源。
        /// </summary>
        internal static System.Drawing.Bitmap warning {
            get {
                object obj = ResourceManager.GetObject("warning", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查詢類型 System.Byte[] 的當地語系化資源。
        /// </summary>
        internal static byte[] 高雄市國中畢業生進路統計報表樣版 {
            get {
                object obj = ResourceManager.GetObject("高雄市國中畢業生進路統計報表樣版", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
