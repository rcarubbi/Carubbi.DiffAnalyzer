using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Carubbi.DiffAnalyzer.UI
{
    [ToolboxData("<{0}:DiffComparisonViewer runat=server></{0}:DiffComparisonViewer>")]
    public class DiffComparisonViewer : DataBoundControl
    {
        private readonly Table tbl = new Table();

        public new List<DiffComparison> DataSource
        {
            set => base.DataSource = value;
            get => (List<DiffComparison>)base.DataSource;
        }

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

        private void CreateCell(TableCell cell, TableRow row, bool visible, string text, FontStyle? style = null)
        {
            if (!visible) return;
            cell = new TableCell();
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
                        break;
                }
            }
            cell.Text = text;
            row.Cells.Add(cell);
        }

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

        protected override void PerformDataBinding(IEnumerable retrievedData)
        {
            if (DesignMode)
            {
                tbl.Rows.Clear();
            }

            base.PerformDataBinding(retrievedData);

            // Verify data exists.
            if (retrievedData == null) return;
            ConfigureFont();
            ConfigureBorder();
            ConfigureLayout();

            TableCell cell1 = null,
                cell2 = null,
                cell3 = null,
                cell4 = null,
                cell5 = null,
                cell6 = null,
                cell7 = null,
                cell8 = null;

            var dataStr = string.Empty;

            var titleAlreadyShowed = false;

            foreach (var dataItem in retrievedData)
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
                    comparison = ((DiffComparison)dataItem);
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
                        tbl.Rows.Add(row);
                        row.CssClass = "header-row";
                        if (TitleBackColor.HasValue)
                            row.BackColor = TitleBackColor.Value;
                        if (TitleForeColor.HasValue)
                            row.ForeColor = TitleForeColor.Value;
                        CreateCell(cell1, row, true, PropertyNameTextField, PropertyNameStyle);
                        CreateCell(cell3, row, true, OldValueTextField, OldValueStyle);
                        CreateCell(cell5, row, true, NewValueTextField, NewValueStyle);
                        CreateCell(cell7, row, ShowState, StatusTextField, StatusStyle);
                        titleAlreadyShowed = true;
                    }

                    row = new TableRow();
                    ApplyColors(row, comparison);
                    tbl.Rows.Add(row);

                    CreateCell(cell2, row, true, comparison.PropertyName.PadLeft(comparison.Depth * 3, "&nbsp"));
                    CreateCell(cell4, row, true, comparison.OldValue.ToString());
                    CreateCell(cell6, row, true, comparison.NewValue.ToString());
                    CreateCell(cell8, row, ShowState, GetDiffState(comparison.State).ToString());

                    if (comparison.LastProperty)
                        tbl.Rows[tbl.Rows.Count - 1].CssClass += " separator-row";
                }
                else if (LabelDisposition == LabelDisposition.Column)
                {
                    row = new TableRow();
                    ApplyColors(row, comparison);
                    tbl.Rows.Add(row);
                    CreateCell(cell1, row, ShowPropertyName, PropertyNameTextField, PropertyNameStyle);
                    CreateCell(cell2, row, true, comparison.PropertyName.PadLeft(comparison.Depth * 3, "&nbsp"));
                    CreateCell(cell3, row, ShowOldValueName, OldValueTextField, OldValueStyle);
                    CreateCell(cell4, row, true, comparison.OldValue.ToString());
                    CreateCell(cell5, row, ShowNewValueName, NewValueTextField, NewValueStyle);
                    CreateCell(cell6, row, true, comparison.NewValue.ToString());
                    CreateCell(cell7, row, ShowState, GetDiffState(comparison.State).ToString());
                }
                else
                {
                    row = new TableRow();
                    ApplyColors(row, comparison);
                    tbl.Rows.Add(row);
                    CreateCell(cell2, row, true, comparison.PropertyName.PadLeft(comparison.Depth * 3, "&nbsp"));
                    CreateCell(cell4, row, true, comparison.OldValue.ToString());
                    CreateCell(cell6, row, true, comparison.NewValue.ToString());
                    CreateCell(cell7, row, ShowState, GetDiffState(comparison.State).ToString());
                }
            }

            Controls.Add(tbl);
        }

        private string PutSpaces(string name)
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

            if (b)
                value = "Verdadeiro";
            else if ((bool) value == false)
            {
                value = "Falso";
            }

            return value;
        }

        private object GetDiffState(DiffState enumItem)
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
            tbl.BorderColor = BorderColor;
            tbl.BorderStyle = BorderStyle;
            tbl.BorderWidth = BorderWidth;
        }

        private void ConfigureFont()
        {
            tbl.Font.Bold = Font.Bold;
            tbl.Font.Italic = Font.Italic;
            tbl.Font.Name = Font.Name;
            tbl.Font.Names = Font.Names;
            tbl.Font.Overline = Font.Overline;
            tbl.Font.Size = Font.Size;
            tbl.Font.Strikeout = Font.Strikeout;
            tbl.Font.Underline = Font.Underline;
        }

        private void ConfigureLayout()
        {
            tbl.Width = Width;
            tbl.Height = Height;
            tbl.Enabled = Enabled;
            tbl.CssClass = CssClass;
            tbl.CellPadding = CellPadding;
            tbl.CellSpacing = CellSpacing;
            Thread.CurrentThread.CurrentUICulture = Culture;
        }

        private void ApplyColors(TableRow row, DiffComparison comparison)
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
