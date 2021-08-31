using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// ファンクションボタンコントロールを表します。
    /// </summary>
    public class WCL_FunctionButton : WCL_DisabledFocusButton
    {
        #region メンバ

        private FunctionButtonRoleKind _role = FunctionButtonRoleKind.None;
        private Keys _keys = Keys.None;
        private Image _customImage = null;
        private string _customText = String.Empty;

        [Category("WCL")]
        [DefaultValue(FunctionButtonRoleKind.None)]
        public FunctionButtonRoleKind Role
        {
            get => this._role;
            set
            {
                if (this._role != value)
                {
                    this._role = value;
                    this.Refresh();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(Keys.None)]
        public Keys Keys
        {
            get => this._keys;
            set
            {
                if (this._keys != value)
                {
                    this._keys = value;
                    this.Refresh();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue("")]
        public string CustomText
        {
            get => this._customText;
            set
            {
                if (this._customText?.Equals(value) != true)
                {
                    this._customText = value;
                    this.Refresh();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(null)]
        public Image CustomImage
        {
            get => this._customImage;
            set
            {
                if (this._customImage != value)
                {
                    this._customImage = value;
                    this.Refresh();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(false)]
        public new bool Enabled
        {
            get => base.Enabled;
            set => base.Enabled = value;
        }

        [Category("WCL")]
        [DefaultValue(true)]
        public bool OccurValidateAuthority { get; set; } = true;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text
        {
            get => base.Text;
            protected set => base.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image Image
        {
            get => base.Image;
            protected set => base.Image = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ContentAlignment TextAlign
        {
            get => base.TextAlign;
            protected set => base.TextAlign = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ContentAlignment ImageAlign
        {
            get => base.ImageAlign;
            protected set => base.ImageAlign = value;
        }

        [Category("WCL")]
        public event CancelEventHandler ValidatingAuthority;

        #endregion

        #region メソッド

        public WCL_FunctionButton()
        {
            this.AutoSize = false;
            this.Enabled = false;
            this.TextAlign = ContentAlignment.BottomCenter;
            this.ImageAlign = ContentAlignment.TopCenter;
        }

        public virtual bool ValidateAuthority()
        {
            var e = new CancelEventArgs();
            this.OnValidatingAuthority(e);
            if (e.Cancel)
            {
                return false;
            }
            return true;
        }

        public virtual void ShowConstraintMessageForAuthority()
        {
            Utils.ShowMessage(this.FindForm(), MessageKind.Error, Dependencies.Views.MessageBox.Texts[MessageDetailKind.Error_NoAuthorization]);
        }

        #endregion

        #region イベント

        protected override void OnClick(EventArgs e)
        {
            if (this.OccurValidateAuthority && this.ValidateAuthority() == false)
            {
                this.ShowConstraintMessageForAuthority();
                return;
            }
            base.OnClick(e);
        }

        protected virtual void OnValidatingAuthority(CancelEventArgs e)
        {
            this.ValidatingAuthority?.Invoke(this, e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // 基底の処理
            base.OnPaint(pevent);

            // 役割とキーの検証
            if (this.Role == FunctionButtonRoleKind.None || 
                this.Keys == Keys.None)
            {
                return;
            }

            // キー番号の描画
            pevent.Graphics.DrawString(
                this.Keys.ToString(),
                new Font(this.Font.Name, 8.0F),
                this.Enabled ? Brushes.Black : Brushes.Gray,
                3,
                3);

            // テキストの描画
            string text;
            if (string.IsNullOrEmpty(this.CustomText) == false)
            {
                text = this.CustomText;
            }
            else
            {
                text = Dependencies.Views.FunctionButtons.Texts.ContainsKey(this.Role) ?
                    Dependencies.Views.FunctionButtons.Texts[this.Role]:
                    string.Empty;
            }
            pevent.Graphics.DrawString(
                text,
                this.Font,
                this.Enabled ? Brushes.Black : Brushes.Gray,
                new Rectangle(0, 0, this.Width, this.Height - 3),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });

            // イメージの描画
            Image image;
            if (this.CustomImage != null)
            {
                image = this.CustomImage;
            }
            else
            {
                image = Dependencies.Views.FunctionButtons.Images.ContainsKey(this.Role) ?
                    Dependencies.Views.FunctionButtons.Images[this.Role] :
                    null;
            }
            if (image != null)
            {
                pevent.Graphics.DrawImage(
                    this.Enabled ? image : image.ConvertToGrayScale(),
                    (this.Width - image.Width) / 2,
                    5);
            }
        }

        #endregion
    }
}
