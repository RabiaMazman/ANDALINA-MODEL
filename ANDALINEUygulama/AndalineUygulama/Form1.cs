using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace perseptron
{
    public partial class Perseptron : Form
    {
        double yeniAgirlikW1, yeniAgirlikW2;
        double yeniEsikDegeri;
        public Perseptron()
        {
            InitializeComponent();
        }

        private void btnSonuç_Click_1(object sender, EventArgs e)
        {
            tbCozum.Text = "----ANDALINE Adım Adım Çözümü----" + "\r\n";
            HesapParemetreleri();
        }


        public void HesapParemetreleri()
        {
            bool islem = true;
            yeniAgirlikW1 = Convert.ToDouble(tb_w1.Text);
            yeniAgirlikW2 = Convert.ToDouble(tb_w2.Text);
            yeniEsikDegeri = Convert.ToDouble(tb_EsikDegeri.Text);
            int ornek1Sonuc = 0, ornek2Sonuc = 0;
            int iterasyon = 1;

            while (islem)
            {

                tbCozum.Text += "\r\n" + iterasyon.ToString() + ".İterasyonda " + "Örnek1 ağa gösterilir";
                ornek1Sonuc = Formul(
                    1, iterasyon, Convert.ToDouble(tb_X1_x1.Text), Convert.ToDouble(tb_X1_x2.Text),
                    yeniAgirlikW1, yeniAgirlikW2, yeniEsikDegeri,
                    Convert.ToDouble(tb_OgrenmeKatsayisi.Text), Convert.ToDouble(tbb1.Text)
                    );
                iterasyon++;
                tbCozum.Text += "\r\n" + iterasyon.ToString() + ".İterasyonda " + "Örnek2 ağa gösterilir";
                ornek2Sonuc = Formul(
                    2, iterasyon, Convert.ToDouble(tb_X2_x1.Text), Convert.ToDouble(tb_X2_x2.Text),
                   yeniAgirlikW1, yeniAgirlikW2, yeniEsikDegeri,
                   Convert.ToDouble(tb_OgrenmeKatsayisi.Text), Convert.ToDouble(tbb2.Text)
                   );
                iterasyon++;
                if (ornek1Sonuc == 1 && ornek2Sonuc == 1)
                {

                    islem = false;
                }
            }
            tbCozum.Text += "\r\n" + "Öğrenme Sonunda ağırlıklar:" + "\r\n" + "w1= " + yeniAgirlikW1.ToString() + "\r\n" + "w2= " + yeniAgirlikW2.ToString() ;
        }

        

        public int Formul(int ornekNo, int iterasyon, double x1, double x2, double w1, double w2, double a, double beklenen, double ɸ)
        {
            double net=0;
            net = w1 * x1 + w2 * x2 + ɸ;

            tbCozum.Text += "\r\n" + "NET= w1 * x1 + w2 * x2 + ɸ = " + net.ToString();
            if (net >=0) 
            {
                tbCozum.Text += "\r\n" + "Net > 0 olduğundan Ç" + ornekNo.ToString() + "= 1 olacaktır.";
                if (beklenen == 1)
                {
                    tbCozum.Text += "\r\n" + "Ç" + ornekNo.ToString() + "=B" + ornekNo.ToString() + " olduğundan ağırlıklar değişmez ." + "\r\n";
                    tbCozum.Text += "\r\nw1=" + w1.ToString() + " w2=" + w2.ToString() + "\r\n";
                    return 1;

                }
                else
                {
                    tbCozum.Text += "\r\n" + "Ç" + ornekNo.ToString() + "=!B" + ornekNo.ToString() + " olduğundan ağırlıklar değişir." + "\r\n";

                    yeniAgirlikW1 = w1 + ((beklenen - 1) * a * x1);
                    tbCozum.Text += "\r\n" + "Yeni Ağırlık(W1)=" + w1.ToString() + "+(" +"("+beklenen.ToString()+"-"+ 1.ToString()+")" +  a.ToString() + "*" + x1.ToString() + "\r\n";
                   
                    yeniAgirlikW2 = w2 + ((beklenen - 1) * a * x2);
                    tbCozum.Text += "\r\n" + "Yeni Ağırlık(W2)= " + w2.ToString() + "+(" + "(" + beklenen.ToString() + "-" + 1.ToString() + ")" + a.ToString() + "*" + x1.ToString() + "\r\n";


                    yeniEsikDegeri = ɸ + (a * (beklenen - 1));
                    tbCozum.Text += "\r\n" + "Yeni Eşik değeri(ɸ)= " + ɸ.ToString() + "+(" + a.ToString() +  "*(" + beklenen.ToString() + "-" + 1.ToString() + ")" + ")" + "\r\n";

                    tbCozum.Text += "\r\n" + "-Yeni Değerler-" + "\r\n" + "w1= " + yeniAgirlikW1.ToString() + "\r\n" + "w2= " + yeniAgirlikW2.ToString()  + "\r\n";
                    return -1;
                }
            } 
            else
            {
                tbCozum.Text += "\r\n" + "Net < 0 olduğundan Ç" + ornekNo.ToString() + "= -1 olacaktır.";
                if (beklenen == -1)
                {
                    tbCozum.Text += "\r\n" + "Ç" + ornekNo.ToString() + "=B" + ornekNo.ToString() + " olduğundan ağırlıklar değişmez." + "\r\n";
                    tbCozum.Text += "\r\nw1=" + w1.ToString() + " w2=" + w2.ToString() + "\r\n";
                    return 1;
                }
                else
                {
                    tbCozum.Text += "\r\n" + "Ç" + ornekNo.ToString() + "=!B" + ornekNo.ToString() + " olduğundan ağırlıklar değişir." + "\r\n";

                    yeniAgirlikW1 = w1 + ((beklenen + 1) * a * x1);
                    tbCozum.Text += "\r\n" + "Yeni Ağırlık(W1)= " + w1.ToString() + "+(" + "(" + beklenen.ToString() + "+" + 1.ToString() + ")" + a.ToString() + "*" + x1.ToString() + "\r\n";
                   
                    yeniAgirlikW2 = w2 + ((beklenen + 1) * a * x2);
                    tbCozum.Text += "\r\n" + "Yeni Ağırlık(W2)= " + w2.ToString() + "+(" + "(" + beklenen.ToString() + "+" + 1.ToString() + ")" + a.ToString() + "*" + x1.ToString() + "\r\n";
                   
                    yeniEsikDegeri = ɸ + (a * (beklenen + 1));
                    tbCozum.Text += "\r\n" + "Yeni Eşik değeri(ɸ)= " + ɸ.ToString() + "+(" + a.ToString() + "* (" + beklenen.ToString() + "+" + 1.ToString() + ")" + ")" + "\r\n";

                    tbCozum.Text += "\r\n" + "-Yeni Değerler -" + "\r\n" + "w1= " + yeniAgirlikW1.ToString() + "\r\n" + "w2= " + yeniAgirlikW2.ToString() + "\r\n";
                    return -1;

                }
            }
        }

    }
}

