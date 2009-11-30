#region License
/*	
Microsoft Reciprocal License (Ms-RL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) Reciprocal Grants- For any file you distribute that contains code from the software (in source code or binary format), you must provide recipients the source code to that file along with a copy of this license, which license will govern that file. You may license other files that are entirely your own work and do not contain code from the software under any terms you choose.
(B) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(C) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(D) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(E) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(F) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
*/
#endregion
                   
                
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace EternalPlay.ReusableCore.Web {

    /// <summary>
    /// Provides the base for richly functional text entry controls
    /// 
    /// Primary Features:
    /// 1.  Auto Select
    /// 2.  Label association and placement
    /// 3.  Keystroke Filtering
    /// 4.  Required validation
    /// 5.  Range validation
    /// 6.  Pattern validation
    /// </summary>
    /// <remarks>
    /// Successful label association requires that a label id be provided.  The label will not be able to find the text box otherwise.
    /// Successful error message generation requires that a user friendly name be provided.
    /// </remarks>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:EntryBase runat=server></{0}:EntryBase>")]
    public abstract class EntryBase : CompositeControl, ITextControl {

        #region Fields

        //NOTE:  Child control Id default values
        private const string _entryTextBoxId = "EntryTextBox";
        private const string _patternValidatorId = "EntryPatternValidator";
        private const string _rangeValidatorId = "EntryRangeValidator";
        private const string _requiredValidatorId = "EntryRequiredValidator";

        //NOTE:  Property fields
        private TextBox _entryTextBox;
        private RegularExpressionValidator _patternValidator;
        private RangeValidator _rangeValidator;
        private RequiredFieldValidator _requiredValidator;

        #endregion

        #region Event Handlers

        /// <summary>
        /// Ensure child controls are created.
        /// </summary>
        /// <param name="e">An System.EventArgs object that contains the event data.</param>
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            EnsureChildControls();
        }

        /// <summary>
        /// Prepare the control for full functionality
        /// </summary>
        /// <param name="e">An System.EventArgs object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            InitializeControl();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Id of the label that will be hooked up to the entry control's child text box control.
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(false)]
        public string AssociatedLabelId {
            get {
                String associatedLabelId = (String)ViewState["AssociatedLabelId"];
                return ((associatedLabelId == null)? String.Empty : associatedLabelId);
            }
            set {
                ViewState["AssociatedLabelId"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the display behavior of the error message in validation controls.
        /// </summary>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(ValidatorDisplay.Static)]
        [Localizable(false)]
        public ValidatorDisplay Display {
            get {
                Object display = ViewState["Display"];
                return ((display == null)? ValidatorDisplay.Static : (ValidatorDisplay)display);
            }
            set {
                ViewState["Display"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value enabling\disabling auto slection of text box contents when receiving focus.
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public bool EnableAutoSelect {
            get {
                Object enableAutoSelect = ViewState["EnableAutoSelect"];
                return ((enableAutoSelect == null)? false : (bool)enableAutoSelect);
            }
            set {
                ViewState["EnableAutoSelect"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value enabling\disabling keystroke filtering.
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public bool EnableKeystrokeFiltering {
            get {
                Object enableKeystrokeFiltering = ViewState["EnableKeystrokeFiltering"];
                return ((enableKeystrokeFiltering == null)? false : (bool)enableKeystrokeFiltering);
            }
            set {
                ViewState["EnableKeystrokeFiltering"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the regular expression used to filter keystrokes
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(false)]
        public string KeystrokeFilterExpression {
            get {
                String keystrokeFilterExpression = (String)ViewState["KeystrokeFilterExpression"];
                return ((keystrokeFilterExpression == null)? String.Empty : keystrokeFilterExpression);
            }
            set {
                ViewState["KeystrokeFilterExpression"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating where the error text will be placed.
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue(EternalPlay.ReusableCore.Web.EntryBase.ErrorTextLocation.AdjacentToTextBox)]
        [Localizable(false)]
        public ErrorTextLocation ErrorTextPlacement {
            get {
                Object errorTextPlacement = ViewState["ErrorTextPlacement"];
                return ((errorTextPlacement == null)? ErrorTextLocation.AdjacentToTextBox : (ErrorTextLocation)errorTextPlacement);
            }
            set {
                ViewState["ErrorTextPlacement"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the contents of the Entry control can be changed.
        /// </summary>
        /// <remarks>
        /// No validation will be sent to the client when a control is set to read-only using this property.  
        /// To set the text box to read-only and still get validation you must set the <code>TextBoxReadOnly</code> property to true.
        /// </remarks>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public bool ReadOnly {
            get {
                Object readOnly = ViewState["ReadOnly"];
                return ((readOnly == null)? false : (bool)readOnly);
            }
            set {
                ViewState["ReadOnly"] = value;
            }
        }

        /// <summary>
        /// Gets the Tag Key for a span that contains the text box and validation controls
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        [Browsable(false)]
        protected override HtmlTextWriterTag TagKey {
            get {
                return HtmlTextWriterTag.Span;
            }
        }

        /// <summary>
        /// Gets or sets the user friendly name used to construct error messages
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public string UserFriendlyName {
            get {
                String userFriendlyName = (String)ViewState["UserFriendlyName"];
                return ((userFriendlyName == null)? String.Empty : userFriendlyName);
            }
            set {
                ViewState["UserFriendlyName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the validation group specification for contained validation controls
        /// </summary>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(false)]
        public string ValidationGroup {
            get {
                EnsureChildControls();
                return this.PatternValidator.ValidationGroup;
            }
            set {
                EnsureChildControls();
                this.PatternValidator.ValidationGroup = 
                    this.RangeValidator.ValidationGroup = 
                    this.RequiredValidator.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the text box control.
        /// </summary>
        /// <remarks>Convienant passthrough to the <code>TextBoxWidth</code> property</remarks>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(false)]
        public override Unit Width {
            get {
                return this.TextBoxWidth;
            }
            set {
                this.TextBoxWidth = value;
            }
        }

        #region Child Control Property Proxies

        #region Pattern Validator

        /// <summary>
        /// Provides reference to the pattern validator child control 
        /// </summary>
        protected RegularExpressionValidator PatternValidator {
            get {
                EnsureChildControls();
                return _patternValidator;
            }
            private set {
                _patternValidator = value;
            }
        }

        /// <summary>
        /// Gets or sets the CssClass for the child PatternValidator control
        /// </summary>
        public string PatternValidatorCssClass {
            get {
                EnsureChildControls();
                return _patternValidator.CssClass;
            }
            set {
                EnsureChildControls();
                _patternValidator.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the error message value for the pattern validator
        /// </summary>
        public string PatternValidatorErrorMessage {
            get {
                EnsureChildControls();
                return _patternValidator.ErrorMessage;
            }
            set {
                EnsureChildControls();
                _patternValidator.ErrorMessage = 
                    _patternValidator.ToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the regular expression for the pattern validator
        /// </summary>
        public string PatternValidatorExpression {
            get {
                EnsureChildControls();
                return _patternValidator.ValidationExpression;
            }
            set {
                EnsureChildControls();
                _patternValidator.ValidationExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id for the child validation control
        /// </summary>
        public string PatternValidatorId {
            get {
                EnsureChildControls();
                return _patternValidator.ID;
            }
            set {
                EnsureChildControls();
                _patternValidator.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the text value for the pattern validator
        /// </summary>
        public string PatternValidatorText {
            get {
                EnsureChildControls();
                return _patternValidator.Text;
            }
            set {
                EnsureChildControls();
                _patternValidator.Text = value;
            }
        }

        #endregion

        #region Range Validation

        /// <summary>
        /// Gets or sets the Maximum value for range validation
        /// </summary>
        public string MaximumValue {
            get {
                EnsureChildControls();
                return this.RangeValidator.MaximumValue;
            }
            set {
                EnsureChildControls();
                this.RangeValidator.MaximumValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the Minimum value for range validation
        /// </summary>
        public string MinimumValue {
            get {
                EnsureChildControls();
                return this.RangeValidator.MinimumValue;
            }
            set {
                EnsureChildControls();
                this.RangeValidator.MinimumValue = value;
            }
        }

        /// <summary>
        /// Provides reference to the range validator child control 
        /// </summary>
        protected RangeValidator RangeValidator {
            get {
                EnsureChildControls();
                return _rangeValidator;
            }
            private set {
                _rangeValidator = value;
            }
        }

        /// <summary>
        /// Gets or sets the CssClass for the child RangeValidator control
        /// </summary>
        public string RangeValidatorCssClass {
            get {
                EnsureChildControls();
                return _rangeValidator.CssClass;
            }
            set {
                EnsureChildControls();
                _rangeValidator.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the error message value for the range validator
        /// </summary>
        public string RangeValidatorErrorMessage {
            get {
                EnsureChildControls();
                return _rangeValidator.ErrorMessage;
            }
            set {
                EnsureChildControls();
                _rangeValidator.ErrorMessage =
                    _rangeValidator.ToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id for the child validation control
        /// </summary>
        public string RangeValidatorId {
            get {
                EnsureChildControls();
                return _rangeValidator.ID;
            }
            set {
                EnsureChildControls();
                _rangeValidator.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the text value for the range validator
        /// </summary>
        public string RangeValidatorText {
            get {
                EnsureChildControls();
                return _rangeValidator.Text;
            }
            set {
                EnsureChildControls();
                _rangeValidator.Text = value;
            }
        }

        #endregion

        #region Required Validation

        /// <summary>
        /// Gets or sets value indicating that an entry control text value is required for submission
        /// </summary>
        public bool Required {
            get {
                EnsureChildControls();
                return this.RequiredValidator.Visible;
            }
            set {
                EnsureChildControls();
                this.RequiredValidator.Visible = value;
            }
        }

        /// <summary>
        /// Provides reference to the required field validator child control 
        /// </summary>
        protected RequiredFieldValidator RequiredValidator {
            get {
                EnsureChildControls();
                return _requiredValidator;
            }
            private set {
                _requiredValidator = value;
            }
        }

        /// <summary>
        /// Gets or sets the CssClass for the child RequiredValidator control
        /// </summary>
        public string RequiredValidatorCssClass {
            get {
                EnsureChildControls();
                return _requiredValidator.CssClass;
            }
            set {
                EnsureChildControls();
                _requiredValidator.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the error message value for the required validator
        /// </summary>
        public string RequiredValidatorErrorMessage {
            get {
                EnsureChildControls();
                return _requiredValidator.ErrorMessage;
            }
            set {
                EnsureChildControls();
                _requiredValidator.ErrorMessage = 
                    _requiredValidator.ToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id for the child validation control
        /// </summary>
        public string RequiredValidatorId {
            get {
                EnsureChildControls();
                return _requiredValidator.ID;
            }
            set {
                EnsureChildControls();
                _requiredValidator.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the text value for the required validator
        /// </summary>
        public string RequiredValidatorText {
            get {
                EnsureChildControls();
                return _requiredValidator.Text;
            }
            set {
                EnsureChildControls();
                _requiredValidator.Text = value;
            }
        }

        #endregion

        #region Text Box

        /// <summary>
        /// Provides reference to the child control 
        /// </summary>
        protected TextBox TextBox {
            get {
                EnsureChildControls();
                return _entryTextBox;
            }
            private set {
                _entryTextBox = value;
            }
        }

        /// <summary>
        /// Gets or sets the CssClass for the child control
        /// </summary>
        public string TextBoxCssClass {
            get {
                EnsureChildControls();
                return _entryTextBox.CssClass;
            }
            set {
                EnsureChildControls();
                _entryTextBox.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id for the child control
        /// </summary>
        public string TextBoxId {
            get {
                EnsureChildControls();
                return _entryTextBox.ID;
            }
            set {
                EnsureChildControls();
                _entryTextBox.ID = value;
            }
        }

        /// <summary>
        /// Gets the client Id for the child control
        /// </summary>
        [Browsable(false)]
        public string TextBoxClientId {
            get {
                EnsureChildControls();
                return _entryTextBox.ClientID;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the contents of the TextBox control can be changed.
        /// </summary>
        /// <remarks>
        /// Validation will be sent to the client when a control is set to read-only using this property.  
        /// To set the text box to read-only and not get validation you must set the <code>ReadOnly</code> property to true.
        /// </remarks>
        public bool TextBoxReadOnly {
            get {
                EnsureChildControls();
                return _entryTextBox.ReadOnly;
            }
            set {
                EnsureChildControls();
                _entryTextBox.ReadOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets the tab index of the child control
        /// </summary>
        public short TextBoxTabIndex {
            get {
                EnsureChildControls();
                return _entryTextBox.TabIndex;
            }
            set {
                EnsureChildControls();
                _entryTextBox.TabIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the child control
        /// </summary>
        /// <remarks>Functionality is redundant with the <code>Text</code> property</remarks>
        public string TextBoxText {
            get {
                EnsureChildControls();
                return _entryTextBox.Text;
            }
            set {
                EnsureChildControls();
                _entryTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for the child control
        /// </summary>
        public string TextBoxTooltip {
            get {
                EnsureChildControls();
                return _entryTextBox.ToolTip;
            }
            set {
                EnsureChildControls();
                _entryTextBox.ToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the child control
        /// </summary>
        /// <remarks>Functionality is redundant with the <code>Width</code> property</remarks>
        public Unit TextBoxWidth {
            get {
                EnsureChildControls();
                return _entryTextBox.Width;
            }
            set {
                EnsureChildControls();
                _entryTextBox.Width = value;
            }
        }

        #endregion

        #endregion

        #region ITextControl Members

        /// <summary>
        /// Gets or sets the text box text value.
        /// </summary>
        /// <remarks>Convienant passthrough to the <code>TextBoxText</code> property</remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text {
            get {
                return this.TextBoxText;
            }
            set {
                this.TextBoxText = value;
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Redirect focus requests to the text box
        /// </summary>
        public override void Focus() {
            EnsureChildControls();
            this.TextBox.Focus();
        }

        #region Control Creation Related (most often initiated by a call to EnsureChildControls)

        /// <summary>
        /// Child controls are created, initialized and assigned her or in a function called from here, and NOWHERE else.
        /// </summary>
        protected override void CreateChildControls() {
            Controls.Clear();

            //NOTE:  Entry Text Box
            Controls.Add(this.TextBox = (TextBox)CreateChildControl(ContainedControlType.EntryTextBox));

            //NOTE:  Pattern Validator
            Controls.Add(this.PatternValidator = (RegularExpressionValidator)CreateChildControl(ContainedControlType.PatternValidator));

            //NOTE:  Range Validator
            Controls.Add(this.RangeValidator = (RangeValidator)CreateChildControl(ContainedControlType.RangeValidator));

            //NOTE:  Required Validator
            Controls.Add(this.RequiredValidator = (RequiredFieldValidator)CreateChildControl(ContainedControlType.RequiredValidator));

            //NOTE:  Validation and Label hook-up with the text box
            AssociateChildControls();

            this.ChildControlsCreated = true;
        }

        /// <summary>
        /// Create, initialize and return individual child controls
        /// </summary>
        /// <param name="controlType">Type of control to create</param>
        /// <returns>Created control</returns>
        protected virtual Control CreateChildControl(ContainedControlType controlType) {
            switch (controlType) {
                case ContainedControlType.EntryTextBox:
                    TextBox textBox = new TextBox();
                    textBox.ID = _entryTextBoxId;
                    textBox.EnableViewState = true;
                    textBox.Visible = true;
                    return textBox;

                case ContainedControlType.PatternValidator:
                    RegularExpressionValidator patternValidator = new RegularExpressionValidator();
                    patternValidator.ID = _patternValidatorId;
                    patternValidator.Text = ControlResources.Entry_Pattern_Text;
                    patternValidator.EnableViewState = true;
                    patternValidator.Visible = true;
                    return patternValidator;

                case ContainedControlType.RangeValidator:
                    RangeValidator rangeValidator = new RangeValidator();
                    rangeValidator.ID = _rangeValidatorId;
                    rangeValidator.Text = ControlResources.Entry_Range_Text;
                    rangeValidator.EnableViewState = true;
                    rangeValidator.Visible = false;     //NOTE:  Defaults to inactive - activiated only if consuming code provides both a Min and Max value.
                    return rangeValidator;

                case ContainedControlType.RequiredValidator:
                    RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                    requiredValidator.ID = _requiredValidatorId;
                    requiredValidator.Text = ControlResources.Entry_Required_Text;
                    requiredValidator.EnableViewState = true;
                    requiredValidator.Visible = false;  //NOTE:  Required=false is the default.  Consuming code must explicitely set Required=true
                    return requiredValidator;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Associate child validation controls with the child textbox control
        /// </summary>
        protected virtual void AssociateChildControls() {
            this.PatternValidator.ControlToValidate = 
                this.RangeValidator.ControlToValidate = 
                this.RequiredValidator.ControlToValidate = this.TextBox.ID;
        }

        #endregion

        #region Neutral Initialization

        /// <summary>
        /// Provides client side script to hook up the provided label with the child text box control.
        /// If an associated label id was not provided the method quietly short-circuits.
        /// </summary>
        protected virtual void AssociateLabelWithTextBox() {
            if (!String.IsNullOrEmpty(this.AssociatedLabelId)) {
                //NOTE:  Register Client Script Resources
                ControlScriptResources.RegisterScriptResource(ClientScriptType.CrossBrowserFunctions, this);
                ControlScriptResources.RegisterScriptResource(ClientScriptType.EntryControls, this);

                //NOTE:  Get a reference to the associated label so that its client id can be resolved
                Control associatedLabel = this.NamingContainer.FindControl(this.AssociatedLabelId);
                if (associatedLabel == null)
                    throw new EntryException(WebExceptionResources.Entry_AssociatedLabelNotFound_MessageText);

                //NOTE:  This function call will go to the client once for every unique base entry control
                string instanceFunctionCall = "AssociateLabelWithTextBox('" + associatedLabel.ClientID + "','" + this.TextBox.ClientID + "');";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID, instanceFunctionCall, true);
            }
        }

        /// <summary>
        /// Check that required attributes are set, if not throw an error.
        /// </summary>
        protected virtual void EnforceRequiredAttributes() {
            //NOTE:  UserFriendlyName is requried if the control is editable
            if ((!this.ReadOnly) && (String.IsNullOrEmpty(this.UserFriendlyName)))
                throw new EntryException(WebExceptionResources.Entry_UserFriendlyName_MessageText);

            //NOTE:  KeystrokeFilterExpression is required if keystroke filtering is turned on
            if ((this.EnableKeystrokeFiltering) && (String.IsNullOrEmpty(this.KeystrokeFilterExpression)))
                throw new EntryException(WebExceptionResources.Entry_KeystrokeFilterExpression_MessageText);

            //NOTE:  MinimumValue and MaximumValue are required to both be present or absent
            if ((String.IsNullOrEmpty(this.MinimumValue)) != (String.IsNullOrEmpty(this.MaximumValue)))
                throw new EntryException(WebExceptionResources.Entry_MinAndMaxValues_MessageText);

            //NOTE:  ErrorTextPlacement of AdjacentToLabel requires the AssociatedLabelId value be provided
            if (this.ErrorTextPlacement == ErrorTextLocation.AdjacentToLabel && String.IsNullOrEmpty(this.AssociatedLabelId))
                throw new EntryException(WebExceptionResources.Entry_LabelRequiredForPlacement_MessageText);
        }

        /// <summary>
        /// Hooks up client side auto select functionality if AutoSelect is true.
        /// </summary>
        protected virtual void InitializeAutoSelect() {
            if (this.EnableAutoSelect) {
                //NOTE:  This function call will go to the client once for every unique base entry control
                string autoSelectScript = "this.select();";
                this.TextBox.Attributes["onfocus"] = autoSelectScript;
            }
        }

        #endregion

        #region Editable Initialization

        /// <summary>
        /// Hooks up client side keystroke filtering if enabled and an expression has been provided.
        /// </summary>
        protected virtual void InitializeKeystrokeFilter() {
            if (this.EnableKeystrokeFiltering && (!String.IsNullOrEmpty(this.KeystrokeFilterExpression))) {
                //NOTE:  Register Client Script Resources
                ControlScriptResources.RegisterScriptResource(ClientScriptType.CrossBrowserFunctions, this);
                ControlScriptResources.RegisterScriptResource(ClientScriptType.EntryControls, this);

                //NOTE:  This function call will go to the client once for every unique entry control
                string keystrokeFilterCall = "IntegerEntry_OnKeyPress(event,'" + this.KeystrokeFilterExpression + "');";
                this.TextBox.Attributes["onkeypress"] = keystrokeFilterCall;
            }
        }

        /// <summary>
        /// Initializes validator controls.
        /// 1. Set display value uniformly on all validators
        /// 2. Enable Range validator if Min and Max are both present
        /// 2.a Assign Range error message if appropriate
        /// 2.b Apply pattern validation expressions when Min and Max have been provided and no other expression has been provided 
        /// 3. Assign Pattern error message if appropriate
        /// 4. Assign Required error message if appropriate
        /// </summary>
        protected virtual void InitializeValidators() {
            string name = this.UserFriendlyName;

            //NOTE:  1. Set display value uniformly on all validators
            this.PatternValidator.Display = 
                    this.RangeValidator.Display = 
                    this.RequiredValidator.Display = this.Display;

            //NOTE:  2. Enable Range validator if Min and Max are both present
            if ((!String.IsNullOrEmpty(this.MinimumValue)) && (!String.IsNullOrEmpty(this.MaximumValue))) {
                this.RangeValidator.Visible = true;
                //NOTE:  2.a Assign Range error message if appropriate: not already set
                if (String.IsNullOrEmpty(this.RangeValidatorErrorMessage))
                    this.RangeValidatorErrorMessage = name + ControlResources.Entry_Range_ErrorMessage1 + this.MinimumValue + ControlResources.Entry_Range_ErrorMessage2 + this.MaximumValue;

                //NOTE:  2.b Apply pattern validation expressions when Min and Max have been provided and no other expression has been provided
                switch (this.RangeValidator.Type) {
                    case ValidationDataType.Integer:
                        if (String.IsNullOrEmpty(this.PatternValidatorExpression))
                            this.PatternValidatorExpression = (int.Parse(this.RangeValidator.MinimumValue, CultureInfo.CurrentCulture) < 0) ? ControlResources.Entry_Pattern_Integer_Expression : ControlResources.Entry_Pattern_IntegerPositive_Expression;
                        if (String.IsNullOrEmpty(this.PatternValidatorErrorMessage))
                            this.PatternValidatorErrorMessage = this.UserFriendlyName + ((int.Parse(this.RangeValidator.MinimumValue, CultureInfo.CurrentCulture) < 0) ? ControlResources.Entry_Pattern_Integer_ErrorMessage : ControlResources.Entry_Pattern_IntegerPositive_ErrorMessage);
                        break;
                }
            }

            //NOTE:  3. Assign Pattern error message if appropriate: not already set and an expression has been set
            if (String.IsNullOrEmpty(this.PatternValidatorErrorMessage) && (!String.IsNullOrEmpty(this.PatternValidatorExpression))) {
                this.PatternValidatorErrorMessage = ControlResources.Entry_Pattern_ErrorMessage;
            }

            //NOTE:  4. Assign Required error message if appropriate:  must be required and not already have a required error message
            if (this.Required && String.IsNullOrEmpty(this.RequiredValidatorErrorMessage))
                this.RequiredValidatorErrorMessage = name + ControlResources.Entry_Required_ErrorMessage;

            //NOTE:  5. Ensure Pattern Validator is visible
            if (this.PatternValidatorExpression != null)
                this.PatternValidator.Visible = true;
        }

        /// <summary>
        /// Place the error text at the location specified
        /// </summary>
        protected virtual void PlaceErrorText() {
            if (this.ErrorTextPlacement == ErrorTextLocation.AdjacentToLabel) {
                //NOTE:  Get a reference to the associated label so that its client id can be resolved
                Control associatedLabel = this.NamingContainer.FindControl(this.AssociatedLabelId);
                if (associatedLabel == null)
                    throw new EntryException(WebExceptionResources.Entry_AssociatedLabelNotFound_MessageText);

                //NOTE:  This function call will go to the client once for every unique base entry control
                StringBuilder clientScript = new StringBuilder();
                if (this.Required)
                    clientScript.Append("MoveErrorTextAdjacentToLabel('" + this.RequiredValidator.ClientID + "','" + associatedLabel.ClientID + "');");
                if (!String.IsNullOrEmpty(this.PatternValidatorExpression))
                    clientScript.Append("MoveErrorTextAdjacentToLabel('" + this.PatternValidator.ClientID + "','" + associatedLabel.ClientID + "');");
                if ((!String.IsNullOrEmpty(this.MinimumValue)) && (!String.IsNullOrEmpty(this.MaximumValue)))
                    clientScript.Append("MoveErrorTextAdjacentToLabel('" + this.RangeValidator.ClientID + "','" + associatedLabel.ClientID + "');");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "PlaceErrorText", clientScript.ToString(), true);
            }
        }

        #endregion

        #endregion

        #region Functions

        /// <summary>
        /// Perform control initialization.  Short circuits if already done and no subseqent changes to indirectly set property values.
        /// </summary>
        private void InitializeControl() {
            NeutralInitialization();
            if (this.ReadOnly)
                ReadOnlyInitialization();
            else
                EditableInitialization();
        }

        /// <summary>
        /// Perform initialization that needs to be done regardless of Editable vs. ReadOnly status
        /// </summary>
        private void NeutralInitialization() {
            EnforceRequiredAttributes();
            AssociateLabelWithTextBox();
            InitializeAutoSelect();
        }

        /// <summary>
        /// Perform initialization for an editable entry control
        /// </summary>
        private void EditableInitialization() {
            this.TextBox.ReadOnly = false;

            InitializeKeystrokeFilter();
            InitializeValidators();
            PlaceErrorText();
        }

        /// <summary>
        /// Set the text box as read only and turn off validators so they are not delivered to the client
        /// </summary>
        private void ReadOnlyInitialization() {
            this.TextBox.ReadOnly = true;
            this.PatternValidator.Visible = 
                    this.RangeValidator.Visible = 
                    this.RequiredValidator.Visible = false;
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Enumerated control types contained within the composite control.  Ony one exists at a time
        /// </summary>
        //VALIDSUPRESSION:  See documentation, nested enumerators are exempt from this rule
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum ContainedControlType {
            /// <summary>
            /// RegularExpressionValidator control
            /// </summary>
            PatternValidator,
            /// <summary>
            /// RangeValidator control
            /// </summary>
            RangeValidator,
            /// <summary>
            /// RequiredFieldValidator control
            /// </summary>
            RequiredValidator,
            /// <summary>
            /// TextBox control
            /// </summary>
            EntryTextBox
        }

        /// <summary>
        /// Value indicating where the error text should be located
        /// </summary>
        //VALIDSUPRESSION:  See documentation, nested enumerators are exempt from this rule
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum ErrorTextLocation {
            /// <summary>
            /// Error text will be placed adjacent to the text box control
            /// </summary>
            AdjacentToTextBox,
            /// <summary>
            /// Error text will be place adjacent to the associated label
            /// </summary>
            AdjacentToLabel
        }

        #endregion

    }
}
