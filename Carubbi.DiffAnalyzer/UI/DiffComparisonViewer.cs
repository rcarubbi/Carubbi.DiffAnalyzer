using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using Carubbi.Extensions;

namespace Carubbi.DiffAnalyzer.UI
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxData("<{0}:DiffComparisonViewer runat=server></{0}:DiffComparisonViewer>")]
    public class DiffComparisonViewer : DataBoundControl
    {
        private readonly Table _table = new Table();

        /// <summary>
        /// 
        /// </summary>
        public new List<DiffComparison> DataSource
        {
            set => base.DataSource = value;
            get => (List<DiffComparison>)base.DataSource;
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? TitleBackColor
        {
            get
            {
                Color? o = null;
                if (ViewState["TitleBackColor"] != null)
                    o = (Color)ViewState["TitleBackColor"];
                return o;
            }
            set
            {
                ViewState["TitleBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? TitleForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["TitleForeColor"] != null)
                    o = (Color)ViewState["TitleForeColor"];
                return o;
            }
            set
            {
                ViewState["TitleForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? ModifiedBackColor
        {
            get
            {
                Color? o = null;
                if (ViewState["ModifiedBackColor"] != null)
                    o = (Color)ViewState["ModifiedBackColor"];
                return o;
            }
            set
            {
                ViewState["ModifiedBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? ModifiedForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["ModifiedForeColor"] != null)
                    o = (Color)ViewState["ModifiedForeColor"];
                return o;
            }
            set
            {
                ViewState["ModifiedForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? AddedBackColor
        {
            get
            {
                Color? o = null;
                if (ViewState["AddedBackColor"] != null)
                    o = (Color)ViewState["AddedBackColor"];
                return o;
            }
            set
            {
                ViewState["AddedBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? AddedForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["AddedForeColor"] != null)
                    o = (Color)ViewState["AddedForeColor"];
                return o;
            }
            set
            {
                ViewState["AddedForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? DeletedBackColor
        {
            get
            {
                Color? o = null;

                if (ViewState["DeletedBackColor"] != null)
                    o = (Color)ViewState["DeletedBackColor"];
                return o;
            }
            set
            {
                ViewState["DeletedBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? DeletedForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["DeletedForeColor"] != null)
                    o = (Color)ViewState["DeletedForeColor"];
                return o;
            }
            set
            {
                ViewState["DeletedForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? NotChangedBackColor
        {
            get
            {
                Color? o = null;
                if (ViewState["NotChangedBackColor"] != null)
                    o = (Color)ViewState["NotChangedBackColor"];
                return o;
            }
            set
            {
                ViewState["NotChangedBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? NotChangedForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["NotChangedForeColor"] != null)
                    o = (Color)ViewState["NotChangedForeColor"];
                return o;
            }
            set
            {
                ViewState["NotChangedForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? UnknowBackColor
        {
            get
            {
                Color? o = null;
                if (ViewState["UnknowBackColor"] != null)
                    o = (Color)ViewState["UnknowBackColor"];
                return o;
            }
            set
            {
                ViewState["UnknowBackColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? UnknowForeColor
        {
            get
            {
                Color? o = null;
                if (ViewState["UnknowForeColor"] != null)
                    o = (Color)ViewState["UnknowForeColor"];
                return o;
            }
            set
            {
                ViewState["UnknowForeColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public string PropertyNameTextField
        {
            get
            {
                var o = ViewState["PropertyNameTextField"];
                return ((o == null) ? Resources.PropertyNameTextField : (string)o);
            }
            set
            {
                ViewState["PropertyNameTextField"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public string NewValueTextField
        {
            get
            {
                var o = ViewState["NewValueTextField"];
                return ((o == null) ? Resources.NewValueTextField : (string)o);
            }
            set
            {
                ViewState["NewValueTextField"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public string OldValueTextField
        {
            get
            {
                var o = ViewState["OldValueTextField"];
                return ((o == null) ? Resources.OldValueTextField : (string)o);
            }
            set
            {
                ViewState["OldValueTextField"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public string StatusTextField
        {
            get
            {
                var o = ViewState["StatusTextField"];
                return ((o == null) ? Resources.StatusTextField : (string)o);
            }
            set
            {
                ViewState["StatusTextField"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color? CellBorderColor
        {
            get
            {
                Color? o = null;
                if (ViewState["CellBorderColor"] != null)
                    o = (Color)ViewState["CellBorderColor"];
                return o;
            }
            set
            {
                ViewState["CellBorderColor"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public BorderStyle? CellBorderStyle
        {
            get
            {
                BorderStyle? o = null;
                if (ViewState["CellBorderStyle"] != null)
                    o = (BorderStyle)ViewState["CellBorderStyle"];
                return o;
            }
            set
            {
                ViewState["CellBorderStyle"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Unit), "")]
        public Unit? CellBorderWidth
        {
            get
            {
                Unit? o = null;
                if (ViewState["CellBorderWidth"] != null)
                    o = (Unit)ViewState["CellBorderWidth"];
                return o;
            }
            set
            {
                ViewState["CellBorderWidth"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public int CellSpacing
        {
            get
            {
                var o = ViewState["CellSpacing"];
                return (int?) o ?? 0;
            }
            set
            {
                ViewState["CellSpacing"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public int CellPadding
        {
            get
            {
                var o = ViewState["CellPadding"];
                return (int?) o ?? 0;
            }
            set
            {
                ViewState["CellPadding"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public FontStyle? OldValueStyle
        {
            get
            {
                FontStyle? o = null;
                if (ViewState["OldValueStyle"] != null)
                    o = (FontStyle)ViewState["OldValueStyle"];
                return o;
            }
            set
            {
                ViewState["OldValueStyle"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public FontStyle? NewValueStyle
        {
            get
            {
                FontStyle? o = null;
                if (ViewState["NewValueStyle"] != null)
                    o = (FontStyle)ViewState["NewValueStyle"];
                return o;
            }
            set
            {
                ViewState["NewValueStyle"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public FontStyle? PropertyNameStyle
        {
            get
            {
                FontStyle? o = null;
                if (ViewState["PropertyNameStyle"] != null)
                    o = (FontStyle)ViewState["PropertyNameStyle"];
                return o;
            }
            set
            {
                ViewState["PropertyNameStyle"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public FontStyle? StatusStyle
        {
            get
            {
                FontStyle? o = null;
                if (ViewState["StatusStyle"] != null)
                    o = (FontStyle)ViewState["StatusStyle"];
                return o;
            }
            set
            {
                ViewState["StatusStyle"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public LabelDisposition LabelDisposition
        {
            get
            {
                var o = ViewState["LabelDisposition"];
                return (LabelDisposition?) o ?? LabelDisposition.Column;
            }
            set
            {
                ViewState["LabelDisposition"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public bool PutSpacesInPropertyName
        {
            get
            {
                var o = ViewState["PutSpacesInPropertyName"];
                return ((o != null) && (bool)o);
            }
            set
            {
                ViewState["PutSpacesInPropertyName"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public bool ShowPropertyName
        {
            get
            {
                var o = ViewState["ShowPropertyName"];
                return (bool?) o ?? true;
            }
            set
            {
                ViewState["ShowPropertyName"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public bool ShowNewValueName
        {
            get
            {
                var o = ViewState["ShowNewValueName"];
                return (bool?) o ?? true;
            }
            set
            {
                ViewState["ShowNewValueName"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public bool ShowOldValueName
        {
            get
            {
                var o = ViewState["ShowOldValueName"];
                return ((o == null) || (bool)o);
            }
            set
            {
                ViewState["ShowOldValueName"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public bool ShowState
        {
            get
            {
                var o = ViewState["ShowState"];
                return (bool?) o ?? false;
            }
            set
            {
                ViewState["ShowState"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        public CultureInfo Culture
        {
            get
            {
                var o = ViewState["Culture"];
                return ((o == null) ? CultureInfo.GetCultureInfo("en-US") : (CultureInfo)o);
            }
            set
            {
                ViewState["Culture"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Propriedade BitWise que recebe os estados que deverão ser exibidos no vizualizador, </para>
        /// <para>caso vazio ele assume que todos os estados devem ser exibidos como padrão</para>
        /// </summary>
        [Category("Behavior")]
        public DiffState StateFilter
        {
            get
            {
                var o = ViewState["ShowStates"];
                return (DiffState?) o ?? DiffState.Added | DiffState.Deleted | DiffState.Modified | DiffState.NotChanged | DiffState.Unknow;
            }
            set
            {
                ViewState["ShowStates"] = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }
     
        /// <inheritdoc />
        protected override void PerformSelect()
        {
            // Call OnDataBinding here if bound to a data source using the
            // DataSource property (instead of a DataSourceID), because the
            // databinding statement is evaluated before the call to GetData.       
            if (!IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }

            // The GetData method retrieves the DataSourceView object from  
            // the IDataSource associated with the data-bound control.            
            GetData().Select(CreateDataSourceSelectArguments(), OnDataSourceViewSelectCallback);

            // The PerformDataBinding method has completed.
            RequiresDataBinding = false;
            MarkAsDataBound();

            // Raise the DataBound event.
            OnDataBound(EventArgs.Empty);
        }

        /// <inheritdoc />
        protected override void PerformDataBinding(IEnumerable retrievedData)
        {
            if (DesignMode)
            {
                _table.Rows.Clear();
            }

            var dataItems = retrievedData as object[] ?? retrievedData?.Cast<object>().ToArray();
            base.PerformDataBinding(dataItems);

            ConfigureFont();
            ConfigureBorder();
            ConfigureLayout();

            var titleAlreadyShowed = false;

            if (dataItems != null)
                foreach (var dataItem in dataItems)
                {
                    DiffComparison comparison;

                    if (DesignMode)
                    {
                        comparison = new DiffComparison
                        {
                            NewValue = "abc",
                            OldValue = "abc",
                            PropertyName = "PropertyName"
                        };
                    }
                    else
                    {
                        comparison = ((DiffComparison) dataItem);
                        if (!StateFilter.Has(comparison.State))
                            continue;

                        if (PutSpacesInPropertyName)
                            comparison.PropertyName = PutSpaces(comparison.PropertyName);

                        comparison.NewValue = TranslateData(comparison.NewValue);
                        comparison.OldValue = TranslateData(comparison.OldValue);
                    }

                    TableRow row;
                    if (LabelDisposition == LabelDisposition.Row)
                    {
                        if (!titleAlreadyShowed)
                        {
                            row = new TableRow();
                            _table.Rows.Add(row);
                            row.CssClass = "header-row";
                            if (TitleBackColor.HasValue)
                                row.BackColor = TitleBackColor.Value;
                            if (TitleForeColor.HasValue)
                                row.ForeColor = TitleForeColor.Value;
                            CreateCell(row, true, PropertyNameTextField, PropertyNameStyle);
                            CreateCell(row, true, OldValueTextField, OldValueStyle);
                            CreateCell(row, true, NewValueTextField, NewValueStyle);
                            CreateCell(row, ShowState, StatusTextField, StatusStyle);
                            titleAlreadyShowed = true;
                        }

                        row = new TableRow();
                        ApplyColors(row, comparison);
                        _table.Rows.Add(row);

                        CreateCell(row, true, comparison.PropertyName.PadLeftWithPattern(comparison.Depth * 3, "&nbsp"));
                        CreateCell(row, true, comparison.OldValue.ToString());
                        CreateCell(row, true, comparison.NewValue.ToString());
                        CreateCell(row, ShowState, GetDiffState(comparison.State).ToString());

                        if (comparison.LastProperty)
                            _table.Rows[_table.Rows.Count - 1].CssClass += " separator-row";
                    }
                    else if (LabelDisposition == LabelDisposition.Column)
                    {
                        row = new TableRow();
                        ApplyColors(row, comparison);
                        _table.Rows.Add(row);
                        CreateCell(row, ShowPropertyName, PropertyNameTextField, PropertyNameStyle);
                        CreateCell(row, true, comparison.PropertyName.PadLeftWithPattern(comparison.Depth * 3, "&nbsp"));
                        CreateCell(row, ShowOldValueName, OldValueTextField, OldValueStyle);
                        CreateCell(row, true, comparison.OldValue.ToString());
                        CreateCell(row, ShowNewValueName, NewValueTextField, NewValueStyle);
                        CreateCell(row, true, comparison.NewValue.ToString());
                        CreateCell(row, ShowState, GetDiffState(comparison.State).ToString());
                    }
                    else
                    {
                        row = new TableRow();
                        ApplyColors(row, comparison);
                        _table.Rows.Add(row);
                        CreateCell(row, true, comparison.PropertyName.PadLeftWithPattern(comparison.Depth * 3, "&nbsp"));
                        CreateCell(row, true, comparison.OldValue.ToString());
                        CreateCell(row, true, comparison.NewValue.ToString());
                        CreateCell(row, ShowState, GetDiffState(comparison.State).ToString());
                    }
                }

            Controls.Add(_table);
        }

        private void CreateCell(TableRow row, bool visible, string text, FontStyle? style = null)
        {
            if (!visible) return;
            var cell = new TableCell();
            if (CellBorderColor.HasValue)
                cell.BorderColor = CellBorderColor.Value;
            if (CellBorderStyle.HasValue)
                cell.BorderStyle = CellBorderStyle.Value;
            if (CellBorderWidth.HasValue)
                cell.BorderWidth = CellBorderWidth.Value;
            if (style.HasValue)
            {
                switch (style.Value)
                {
                    case FontStyle.Bold:
                        cell.Font.Bold = true;
                        break;
                    case FontStyle.Italic:
                        cell.Font.Italic = true;
                        break;
                    case FontStyle.Regular:
                        cell.Font.Bold =
                            cell.Font.Italic =
                                cell.Font.Strikeout =
                                    cell.Font.Underline = false;
                        break;
                    case FontStyle.Strikeout:
                        cell.Font.Strikeout = true;
                        break;
                    case FontStyle.Underline:
                        cell.Font.Underline = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            cell.Text = text;
            row.Cells.Add(cell);
        }

        private static string PutSpaces(string name)
        {
            var spaceIndexes = new List<int>();
            var indexCounter = 0;
            foreach (var c in name)
            {
                if (char.IsUpper(c))
                {
                    spaceIndexes.Add(indexCounter);
                }
                indexCounter++;
            }
            var spaceCounter = 0;
            foreach (var index in spaceIndexes)
            {
                if (index <= 0) continue;
                name = name.Insert(index + spaceCounter, " ");
                spaceCounter++;
            }
            return name;
        }

        private object TranslateData(object value)
        {
            if (Culture.Name != "pt-BR") return value;
            if (!(value is bool b)) return value;
            return b ? "Verdadeiro" : "Falso"; ;
        }

        private static object GetDiffState(DiffState enumItem)
        {
            switch (enumItem)
            {
                case DiffState.Unknow:
                    return Resources.DiffState_Unknow;
                case DiffState.NotChanged:
                    return Resources.DiffState_NotChanged;
                case DiffState.Modified:
                    return Resources.DiffState_Modified;
                case DiffState.Added:
                    return Resources.DiffState_Added;
                case DiffState.Deleted:
                    return Resources.DiffState_Deleted;
                default:
                    return Resources.DiffState_Unknow;
            }
        }

        private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
        {
            // Call OnDataBinding only if it has not already been 
            // called in the PerformSelect method.
            if (IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }
            // The PerformDataBinding method binds the data in the  
            // retrievedData collection to elements of the data-bound control.
            PerformDataBinding(retrievedData);
        }

        private void ConfigureBorder()
        {
            _table.BorderColor = BorderColor;
            _table.BorderStyle = BorderStyle;
            _table.BorderWidth = BorderWidth;
        }

        private void ConfigureFont()
        {
            _table.Font.Bold = Font.Bold;
            _table.Font.Italic = Font.Italic;
            _table.Font.Name = Font.Name;
            _table.Font.Names = Font.Names;
            _table.Font.Overline = Font.Overline;
            _table.Font.Size = Font.Size;
            _table.Font.Strikeout = Font.Strikeout;
            _table.Font.Underline = Font.Underline;
        }

        private void ConfigureLayout()
        {
            _table.Width = Width;
            _table.Height = Height;
            _table.Enabled = Enabled;
            _table.CssClass = CssClass;
            _table.CellPadding = CellPadding;
            _table.CellSpacing = CellSpacing;
            Thread.CurrentThread.CurrentUICulture = Culture;
        }

        private void ApplyColors(WebControl row, DiffComparison comparison)
        {
            switch (comparison.State)
            {
                case DiffState.Added:
                    row.CssClass = "bg-novo";
                    if (AddedBackColor.HasValue)
                        row.BackColor = AddedBackColor.Value;
                    if (AddedForeColor.HasValue)
                        row.ForeColor = AddedForeColor.Value;
                    break;
                case DiffState.Deleted:
                    row.CssClass = "bg-excluido";
                    if (DeletedBackColor.HasValue)
                        row.BackColor = DeletedBackColor.Value;
                    if (DeletedForeColor.HasValue)
                        row.ForeColor = DeletedForeColor.Value;
                    break;
                case DiffState.Modified:
                    row.CssClass = "bg-alterado";
                    if (ModifiedBackColor.HasValue)
                        row.BackColor = ModifiedBackColor.Value;
                    if (ModifiedForeColor.HasValue)
                        row.ForeColor = ModifiedForeColor.Value;
                    break;
                case DiffState.NotChanged:
                    row.CssClass = "bg-igual";
                    if (NotChangedBackColor.HasValue)
                        row.BackColor = NotChangedBackColor.Value;
                    if (NotChangedForeColor.HasValue)
                        row.ForeColor = NotChangedForeColor.Value;
                    break;
                case DiffState.Unknow:
                    row.CssClass = "bg-inconclusivo";
                    if (UnknowBackColor.HasValue)
                        row.BackColor = UnknowBackColor.Value;
                    if (UnknowForeColor.HasValue)
                        row.ForeColor = UnknowForeColor.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
