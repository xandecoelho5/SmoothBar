using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum StyleType
{
    Continuous, 
    Marquee
}
public enum ShapeType
{
    Circle,
    Rectangle
}
public enum OrientationType
{
    Horizontal,
    Vertical
}
static class Constants
{
    public const int MaxPercRadius = 80;
    public const int MinPercRadius = 20;
}

namespace SmoothBar
{
    public partial class SmoothBar : UserControl
    {
        public SmoothBar()
        {
            InitializeComponent();
            Load += OnLoaded;
        }

        private int min = 0;
        private int max = 100;
        private int val = 0;
        private int percRadius = 50;
        private int posicaoInicial = 20;
        private int posicaoAtual = 20;
        private int size = 50; //metade da barra
        private Color BarColor = Color.Red; //cor da barra de progresso;
        private Color EspiralColor = Color.Red; //cor da espiral interna 
        private ShapeType _Shape;
        private OrientationType _Orientation;
        private StyleType _Style;
        private bool firstDraw = true;
        private bool running = false;
        private int _Speed = 10;
        private bool startRunning = false;
        
        public Color ProgressBarColor
        {
            get { return BarColor; }

            set
            {
                BarColor = value;
                this.Invalidate();
            }
        }
        public Color ProgressEspiralColor
        {
            get { return EspiralColor; }

            set
            {
                EspiralColor = value;
                this.Invalidate();
            }
        }
        public int PercentualRadius
        {
            get { return percRadius; }

            set
            {
                //Valida para o valor não ser maior que o máximo e nem menor que o mínimo das Constants
                percRadius = value > Constants.MaxPercRadius ? Constants.MaxPercRadius : value < Constants.MinPercRadius ? Constants.MinPercRadius : value;
                this.Invalidate();
            }
        }
        public int Minimum
        {
            get { return min; }

            set
            {
                if (value < 0) { min = 0; }

                if (value > max) { min = value; }

                if (val < min) { val = min; }

                this.Invalidate();
            }
        }
        public int Maximum
        {
            get { return max; }

            set
            {
                if (value < min) { min = value; }

                max = value;

                if (val > max) { val = max; }

                this.Invalidate();
            }
        }
        public int Value
        {
            get { return val; }

            set
            {
                int oldValue = val;
                //Valida para o valor não ser menor que o mínimo e nem maior que o máximo
                val = value < min ? min : value > max ? max : value;
                if (Style != StyleType.Marquee)
                {
                    if (Shape == ShapeType.Rectangle)
                    {
                        if (Orientation == OrientationType.Vertical) InvalidateAreaVertical(oldValue);
                        else InvalidateAreaHorizontal(oldValue);
                    }
                    else this.Invalidate();
                }
            }
        }

        [Description("Modifica o a velocidade da animação do Marquee, em milisegundos."), Category("Behavior"), Browsable(true)]
        public int MarqueeAnimationSpeed
        {
            get { return _Speed; }
            set { _Speed = value;}
        }

        public bool ComecaRodando
        {
            get { return startRunning; }
            set { startRunning = value; }
        }

        /// <summary>
        /// Modifica o formato da Barra de Progresso.
        /// </summary>

        [Description("Modifica o formato da Barra de Progresso."),
         Category("Appearance"),
         DefaultValue(typeof(ShapeType), "Circle"),
         Browsable(true)]
        public ShapeType Shape
        {
            get { return _Shape; }
            set
            {
                if (value == _Shape) return;
                _Shape = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Modifica a orientação da Barra de Progresso Retangular.
        /// </summary>

        [Description("Modifica a orientação da Barra de Progresso Retangular."),
         Category("Appearance"),
         DefaultValue(typeof(OrientationType), "Horizontal"),
         Browsable(true)]
        public OrientationType Orientation
        {
            get { return _Orientation; }
            set
            {
                if (value == _Orientation) return;

                if (((value == OrientationType.Horizontal) && (this.Height > this.Width)) ||
                    ((value == OrientationType.Vertical)   && (this.Width > this.Height)) )
                {
                    TrocaValores();
                }
                _Orientation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Modifica o formato da Barra de Progresso.
        /// </summary>

        [Description("Modifica o estilo da Barra de Progresso."),
         Category("Appearance"),
         DefaultValue(typeof(StyleType), "Continuous"),
         Browsable(true)]
        public StyleType Style
        {
            get { return _Style; }
            set
            {
                if (value == _Style) return;
                _Style = value;
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
             this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brushBar = new SolidBrush(BarColor);
            SolidBrush brushEspiral = new SolidBrush(EspiralColor);

            float percent = (float)(val - min) / (float)(max - min);
            Rectangle rect = this.ClientRectangle;
            if (Shape == ShapeType.Rectangle)
            {
                // Calculate a área que vai ser colorida
                if (Style == StyleType.Marquee)
                {
                    if (Orientation == OrientationType.Vertical)
                    {
                        rect.Height = (int)(size * Height / 100);
                        if (firstDraw)
                            rect.Y = (int)(posicaoInicial * Height / 100);
                        else
                        {
                            rect.Y = (int)(posicaoAtual * Height / 100);
                            posicaoAtual++;
                            if (rect.Y == Height)
                                posicaoAtual = 0;
                        }

                        int espacoSobra = (rect.Y + rect.Height) - Height;
                        if (espacoSobra > 0)
                        {
                            g.FillRectangle(brushBar, 0, 0, rect.Width, espacoSobra); //Desenha no inicio
                            int espacoBranco = Height - rect.Height;
                            g.FillRectangle(new SolidBrush(BackColor), 0, espacoSobra, rect.Width, espacoBranco); //Desenha no final
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(BackColor), 0, 0, rect.Width, rect.Y); //Desenha no inicio
                            int espacoBranco = Height - (rect.Y + rect.Height);
                            g.FillRectangle(new SolidBrush(BackColor), 0, rect.Y + rect.Height, rect.Width, espacoBranco); //Desenha no final
                        }
                    }
                    else
                    {
                        rect.Width = (int)(size * Width / 100);
                        if (firstDraw)
                            rect.X = (int)(posicaoInicial * Width / 100);
                        else
                        {
                            rect.X = (int)(posicaoAtual * Width / 100);
                            posicaoAtual++;
                            if (rect.X == Width)
                                posicaoAtual = 0;
                        }

                        int espacoSobra = (rect.X + rect.Width) - Width;
                        if (espacoSobra > 0)
                        {
                            g.FillRectangle(brushBar, 0, 0, espacoSobra, rect.Height); //Desenha no inicio
                            int espacoBranco = Width - rect.Width;
                            g.FillRectangle(new SolidBrush(BackColor), espacoSobra, 0, espacoBranco, rect.Height); //Desenha no final
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(BackColor), 0, 0, rect.X, rect.Height); //Desenha no inicio
                            int espacoBranco = Width - (rect.X + rect.Width);
                            g.FillRectangle(new SolidBrush(BackColor), rect.X + rect.Width, 0, espacoBranco, rect.Height); //Desenha no final
                        }
                    }
                } 
                else
                {
                    if (Orientation == OrientationType.Vertical)
                    {
                        rect.Height = (int)((float)rect.Height * percent);

                        // inverte o retângulo
                        g.TranslateTransform(0, Height - 1);
                        g.ScaleTransform(1, -1);
                    }
                    else
                    {
                        rect.Width = (int)((float)rect.Width * percent);
                    }
                }
                
                g.FillRectangle(brushBar, rect); //Desenha a barra de progesso

                Draw3DBorderRect(g); // Desenha uma borda 3D
            }
            else
            {
                float graus;

                if (Style == StyleType.Marquee) {
                    int grausIni = (int)((posicaoInicial * 360) / 100);
                    graus = (int)((size * 360) / 100);

                    if (firstDraw)
                        g.FillPie(brushBar, this.ClientRectangle, grausIni, graus);
                    else
                    {
                        int grausAtual = (int)((posicaoAtual * 360) / 100);
                        posicaoAtual++;
                        if (grausAtual == 360)
                            posicaoAtual = 0;

                        g.FillPie(brushBar, this.ClientRectangle, grausAtual, graus);
                    }
                } else
                {
                    //Espiral externa
                    graus = percent * 360;

                    g.FillPie(brushBar, this.ClientRectangle, 0, graus);
                }

                //Espiral interna
                Rectangle r = this.ClientRectangle;
                float widthX = r.Width * percRadius / 100;
                float heightY = r.Height * percRadius / 100;
                float pointX = (r.Width / 2) - (widthX / 2);
                float pointY = (r.Height / 2) - (heightY / 2);

                g.FillEllipse(brushEspiral, pointX, pointY, widthX, heightY);

                Draw3DBorderCircle(g, pointX, pointY, widthX, heightY);
            }

            brushEspiral.Dispose();
            brushBar.Dispose();
            g.Dispose();

            if (firstDraw) firstDraw = false;
        }

        private void TrocaValores()
        {
            int aux = this.Width;
            this.Width = this.Height;
            this.Height = aux;
        }

        private void SetAlturaRetangulo(int nValor, ref Rectangle rect, bool lOldValue = false)
        {
            int perc = 0;
            float percent = ((float)(nValor - min) / (float)(max - min)) * 100;

            if (percent > 0)
                perc = 100 - (int)percent;

            if ((perc == 0) && lOldValue) perc = 100; //pro oldValue ele vai pegar cheio na primeira vez

            rect.Height = (int)((rect.Height * perc) / 100);
        }

        private void InvalidateAreaVertical(int oldValue)
        {
            Rectangle newValueRect = this.ClientRectangle;
            Rectangle oldValueRect = this.ClientRectangle;
            Rectangle updateRect   = new Rectangle();

            // Calcula o valor do novo retângulo 
            SetAlturaRetangulo(val, ref newValueRect);

            // Calcula o valor do retângulo anterior
            SetAlturaRetangulo(oldValue, ref oldValueRect, true);

            // Acha apenas a parte que precisa ser alterada
            if (newValueRect.Height > oldValueRect.Height)
            {
                updateRect.Y = oldValueRect.Size.Height;
                updateRect.Height = newValueRect.Height - oldValueRect.Height;
            }
            else
            {
                updateRect.Y = newValueRect.Size.Height;
                updateRect.Height = oldValueRect.Height - newValueRect.Height;
            }
            
            updateRect.Width = this.Width - 1;

            // Invalidate só a região alterada
            this.Invalidate(updateRect);
        }

        private void SetLarguraRetangulo(int nValor, ref Rectangle rect)
        {
            float percent = (float)(nValor - min) / (float)(max - min);
            rect.Width = (int)((float)rect.Width * percent);
        }
        private void InvalidateAreaHorizontal(int oldValue)
        {
            Rectangle newValueRect = this.ClientRectangle;
            Rectangle oldValueRect = this.ClientRectangle;
            Rectangle updateRect = new Rectangle();

            // Calcula o valor do novo retângulo 
            SetLarguraRetangulo(val, ref newValueRect);

            // Calcula o valor do retângulo anterior
            SetLarguraRetangulo(oldValue, ref oldValueRect);

            // Acha apenas a parte que precisa ser alterada
            if (newValueRect.Width > oldValueRect.Width)
            {
                updateRect.X = oldValueRect.Size.Width;
                updateRect.Width = newValueRect.Width - oldValueRect.Width;
            }
            else
            {
                updateRect.X = newValueRect.Size.Width;
                updateRect.Width = oldValueRect.Width - newValueRect.Width;
            }

            updateRect.Height = this.Height;

            // Invalidate só a região alterada
            this.Invalidate(updateRect);
        }

        private void Draw3DBorderRect(Graphics g)
        {
            int PenWidth = (int)Pens.White.Width;
            Rectangle r = this.ClientRectangle;

            g.DrawLine(Pens.DarkGray, new Point(r.Left, r.Top), new Point(r.Width - PenWidth, r.Top));
            g.DrawLine(Pens.DarkGray, new Point(r.Left, r.Top), new Point(r.Left, r.Height - PenWidth));
            g.DrawLine(Pens.White, new Point(r.Left, r.Height - PenWidth), new Point(r.Width - PenWidth, r.Height - PenWidth));
            g.DrawLine(Pens.White, new Point(r.Width - PenWidth, r.Top), new Point(r.Width - PenWidth, r.Height - PenWidth));
        }

        private void Draw3DBorderCircle(Graphics g, float pointX, float pointY, float widthX, float heightY)
        {
            Rectangle r = this.ClientRectangle;
            g.DrawEllipse(new Pen(Color.Blue, 1.0f), r.X, r.Y, r.Width - 1, r.Height - 1); //Círculo externo
            g.DrawEllipse(new Pen(BarColor, 1.0f), pointX, pointY, widthX, heightY); //Círculo interno
        }

        protected override CreateParams CreateParams
        {
            // vide https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
            get
            {
                CreateParams result = base.CreateParams;
                result.ExStyle |= 0x02000000; // WS_EX_COMPOSITED //Ativa double-buffer para evitar flicker
                return result;
            }
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            if (startRunning)
            {
                startRun();
            }            
        }

        public async void startRun()
        {
            if ((!DesignMode) && (Style == StyleType.Marquee))
            {
                running = true;
                await Task.Run(() =>
                {
                    while (running)
                    {
                        Thread.Sleep(MarqueeAnimationSpeed);
                        this.Invalidate();
                    }
                });
            }
        }

        public bool IsRunning()
        {
            return running;
        }

        public void stopRun()
        {
            running = false;
        }
    }
}
