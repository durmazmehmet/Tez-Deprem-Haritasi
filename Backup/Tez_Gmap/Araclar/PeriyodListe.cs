public class tabloyaBindir
{
    private double magnitud_;
    private double periyot_;
    private string olasi1_;
    private string olasi10_;
    private string olasi20_;
    private string olasi30_;
    private string olasi50_;
    private string olasi75_;
    private string olasi100_;

    public tabloyaBindir(double _mag, double _p, string _o1, string _o10, string _o20, string _o30, string _o50, string _o75, string _o100)
    {
        magnitud_ = _mag;
        periyot_ = _p;
        olasi1_ = _o1;
        olasi10_ = _o10;
        olasi20_ = _o20;
        olasi30_ = _o30;
        olasi50_ = _o50;
        olasi75_ = _o75;
        olasi100_ = _o100;
    }
    public double Büyüklük
    {
        get { return magnitud_; }
    }
    public double Yıllık_Periyot
    {
        get { return periyot_; }
    }
    public string _1_Yıl
    {
        get { return olasi1_; }
    }
    public string _10_Yıl
    {
        get { return olasi10_; }
    }
    public string _20_Yıl
    {
        get { return olasi20_; }
    }
    public string _30_Yıl
    {
        get { return olasi30_; }
    }
    public string _50_Yıl
    {
        get { return olasi50_; }
    }
    public string _75_Yıl
    {
        get { return olasi75_; }
    }
    public string _100_Yıl
    {
        get { return olasi100_; }
    }
}