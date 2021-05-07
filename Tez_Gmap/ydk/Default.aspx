<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tez_Gmap._Default"
    Culture="tr-TR" UICulture="tr-TR" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml">
<head>
<title>01201529 Mezuniyet Tezi</title>
<link href="tablolar.css" rel="stylesheet" type="text/css" />
<style type="text/css">
v\:* {	behavior: url(#default#VML);}
    .style1
    {
    }
</style>

<script type="text/javascript">
// Listeye kordinat ekler
 function addpoint( point ) 
 {
    if (!document.getElementById("iSecAktif").disabled){
    if (document.getElementById("iSecAktif").checked) {
    var benisay = dataparse( document.getElementById("tracedata").value ) ;
    if (benisay.length < 3){
       document.getElementById("tracedata").value += point.toUrlValue() + "\n" ;
       } else if(benisay.length == 3){
       document.getElementById("tracedata").value += point.toUrlValue() + "\n" ;
       document.getElementById("iSecPasif").checked = true;
       document.getElementById("iSecAktif").disabled = true;
       document.getElementById("iSecPasif").disabled = true;       
        datadraw() ;
        } else {}
        }}
}
//İlk girilen noktanın kordinatını son nokta olarak ekleyerek poligonu kapatır
   function dataclose() 
   {
    var wrkar = dataparse( document.getElementById("tracedata").value ) ;
     document.getElementById("tracedata").value += wrkar[0].lat() + "," + wrkar[0].lng() + "\n" ;
   }

//Javascript'in dizeylerler max ve min değerleri bulşmasına yardımcı olmak için
 Array.prototype.max = function(){
    return Math.max.apply({},this)
 }
  Array.prototype.min = function(){
    return Math.min.apply({},this)
 }
//Poligonu çizen fonksiyon
//Bu sayfadaki subgurim_GMap1 nesnesine çizimi gerçekleştir
//GLatLng ile dikdortgen nesnesindewn bulunun
//ve istemciden gelenleri içeren koordinatları subgurim_GMap1 
//nesnesinin tanıyacağı enlem,boylam kordinata çevirir
   function datadraw(){
    var wrkar = [] ;
    var dikdortgen = [] ;
    var _arrlat = [] ;
    var _arrlng = [] ;   
    if ( document.getElementById("tracedata").value ) // tracedata'da veri varsa
    {
    wrkar = dataparse( document.getElementById("tracedata").value ) ;
    for(i=0;i<wrkar.length;i++){
        _arrlat.push(parseFloat(wrkar[i].lat()));
        _arrlng.push(parseFloat(wrkar[i].lng()));        
    }
    //subgurim_GMap1.clearOverlays() ;
    dikdortgen.push(new GLatLng( _arrlat.min(),_arrlng.min() ));
    dikdortgen.push(new GLatLng( _arrlat.max(),_arrlng.min() ));
    dikdortgen.push(new GLatLng( _arrlat.max(),_arrlng.max() ));
    dikdortgen.push(new GLatLng( _arrlat.min(),_arrlng.max() ));
    dikdortgen.push(new GLatLng( _arrlat.min(),_arrlng.min() ));
    document.getElementById("maxmin").value +=  _arrlat.min() + "x" +_arrlng.min() + "x" + _arrlat.max() + "x" +_arrlng.max() ;
    subgurim_GMap1.addOverlay( new GPolyline(dikdortgen,"#ff0000", 3, .8) ) ;
    }
   }
   //harita üzerindeki herşeyi temizler
   //function dataclear() 
   //{
   // document.getElementById("tracedata").value = "" ;
   // document.getElementById("maxmin").value = "" ;
   // subgurim_GMap1.clearOverlays() ;
   //}
// Textarea nesnesinden alınan kordinatları poligon çizimi için parseller
   function dataparse( points )
   {
    points = points.substr(0,points.length - 1) ;
    // Her satır atlatmada değeri kes al
    var linar = points.split("\n") ;				
    var wrkar = [] ;
    var pntar = [] ;
    for (var i = 0; i < linar.length; i++)
    {
    // virgülle ayrılmış değerleri x y kordinatı olarak al
     wrkar = linar[i].split(",",2) ;				
   
     pntar.push( new GLatLng(parseFloat(wrkar[0]), parseFloat(wrkar[1])) ) ; 
    }
    return pntar ;
   }

   </script>
</head>
<body>
<form id="sismo1" runat="server">
  <div>
    <div id="harita" style="text-align:center">
        <cc1:GMap
            ID="GMap1" runat="server" Height="300px" 
                Key="ABQIAAAAhXI5EBjoKlMJHl55ofUpKhQPT9fGZAv7sMGBTaHGVklbTjM4oxRkBsVFCyrhrPWbZ8QG80quZngnhg" 
                Width="400px" enableServerEvents="True" Language="tr" 
                BorderStyle="Solid" BorderWidth="2px"             
            
            
            
            ajaxUpdateProgressMessage="&lt;blink&gt;Konum bilgisi yükleniyor...&lt;/blink&gt;" enableDoubleClickZoom="False" 
            enableHookMouseWheelToZoom="True" onmarkerclick="GMap1_MarkerClick" />
        <table cellpadding="0" cellspacing="0" class="kduzyaziki" style="width: 100%">
<tr>
                <td width="10%">
                    <div align="center">0 - 2.9</div></td>
  <td style="width:10%">
                    <div align="center">3 - 3.9</div></td>
  <td style="width:10%">
                    <div align="center">4 - 4.9</div></td>
  <td style="width:10%">
                    <div align="center">5 - 5.9</div></td>
  <td style="width:10%">
                    <div align="center">6 - 7.9</div></td>
          <td rowspan="2" align="center" valign="middle">
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="362px" /></td>
          <td rowspan="2" align="center" valign="middle" style="width:50%">
              <input id="iSecPasif" name="iSec" type="radio" runat="server" />ALAN 
              SEÇİMİ PASİF<br />
              <input id="iSecAktif" name="iSec" type="radio" runat="server"/>ALAN SEÇİMİ AKTİF<br />
                <asp:CheckBox ID="yerbilgial" runat="server" AutoPostBack="True" 
                    oncheckedchanged="yerbilgial_CheckedChanged" 
                    Text="Tıklanan deprem için yakın yer bilgisini de çek" Enabled="False" />
                          </td>
          </tr>
                  <tr>
                      <td width="5%">
                          <div align="center">
                              <img alt="" src="images/iki.png" style="width: 12px; height: 12px" /></div></td>
                <td>
                          <div align="center">
                              <img alt="" src="images/iki.png" style="width: 12px; height: 12px" /></div></td>
                <td>
                          <div align="center">
                              <img alt="" src="images/dort.png" style="width: 14px; height: 14px" /></div></td>
                <td>
                          <div align="center">
                              <img alt="" src="images/dort.png" style="width: 14px; height: 14px" /></div></td>
                <td>
                          <div align="center">
                              <img alt="" src="images/alti.png" style="width: 17px; height: 17px" /></div></td>
          </tr>
      </table>
    </div>
    <div id="kontroller" style="text-align:center"><hr />
      <asp:Label ID="uyarici1" runat="server" Text="Depremleri harita üzerinde görmek için gerekli değerleri girip &quot;Depremi Göster&quot;'e basınız..." 
            Font-Bold="True" Font-Size="Medium" 
            Height="45px"></asp:Label>
        <br />
      <asp:Panel ID="iPanel1" runat="server">
        <table class="kduzyaziki">
          <tr>
              <td class="style1">
                  <asp:Label ID="yil_label" runat="server" Font-Bold="True" Text="Yıl Aralığı"></asp:Label>
              </td>
              <td>
                  <asp:Label ID="magnitud_label" runat="server" Font-Bold="True" 
                      Text="Deprem Büyüklüğü Aralığı"></asp:Label>
              </td>
              <td>
                  Haritada Gösterilecek<br />
                  Deprem Sayısı</td>
          </tr>
            <tr>
                <td class="style1">
                    <asp:RequiredFieldValidator ID="yil1_Validator" runat="server" 
                        BackColor="#93ADE3" ControlToValidate="yil1" 
                        ErrorMessage="Başlangıç yılını girmelisiniz!" Font-Bold="True">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="yil1_aralikValid" runat="server" BackColor="#93ADE3" 
                        ControlToValidate="yil1" ErrorMessage="Girilen yıl için kayıt bulunmamaktadır!" 
                        Font-Bold="True" MaximumValue="2008" MinimumValue="1900" Type="Integer">*</asp:RangeValidator>
                    <asp:TextBox ID="yil1" runat="server" AutoCompleteType="Search" 
                        BackColor="#93ADE3" CausesValidation="True" MaxLength="4" 
                        style="margin-bottom: 0px" Width="75px">1900</asp:TextBox>
                    <asp:TextBox ID="yil2" runat="server" AutoCompleteType="Search" 
                        BackColor="#FFA6E9" CausesValidation="True" MaxLength="4" Width="75px">2008</asp:TextBox>
                    <asp:RequiredFieldValidator ID="yil2_Validator" runat="server" 
                        BackColor="#FFA6E9" ControlToValidate="yil2" 
                        ErrorMessage="Bitiş yılını girmelisiniz!" Font-Bold="True">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="yil2_aralikValid" runat="server" BackColor="#FFA6E9" 
                        ControlToValidate="yil2" ErrorMessage="Girilen yıl için kayıt bulunmamaktadır!" 
                        Font-Bold="True" MaximumValue="2008" MinimumValue="1900" Type="Integer">*</asp:RangeValidator>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="m1_Validator" runat="server" 
                        BackColor="#93ADE3" ControlToValidate="magnitud1" 
                        ErrorMessage="Başlangıç şiddet değerini girmelisiniz!" Font-Bold="True">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="m1_aralikValid" runat="server" BackColor="#93ADE3" 
                        ControlToValidate="magnitud1" ErrorMessage="Şiddeti 1 - 7.9 arası seçiniz!" 
                        Font-Bold="True" MaximumValue="8" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                    <asp:TextBox ID="magnitud1" runat="server" BackColor="#93ADE3" 
                        CausesValidation="True" MaxLength="3" style="margin-top: 0px" Width="75px">1</asp:TextBox>
                    <asp:TextBox ID="magnitud2" runat="server" BackColor="#FFA6E9" 
                        CausesValidation="True" MaxLength="3" Width="75px">8</asp:TextBox>
                    <asp:RequiredFieldValidator ID="m2_Validator" runat="server" 
                        BackColor="#FFA6E9" ControlToValidate="magnitud2" 
                        ErrorMessage="Bitiş şiddet değerini girmelisiniz!" Font-Bold="True">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="m2_aralikValid" runat="server" BackColor="#FFA6E9" 
                        ControlToValidate="magnitud2" ErrorMessage="Şiddeti 1 - 7.9 arası seçiniz!" 
                        Font-Bold="True" MaximumValue="8" MinimumValue="0" Type="Double">*</asp:RangeValidator>
                </td>
                <td>
                    <asp:DropDownList ID="MarkerLimit" runat="server" Width="136px">
                        <asp:ListItem Value="1500">1500 ~15sn</asp:ListItem>
                        <asp:ListItem Value="1250">1250 ~12sn</asp:ListItem>
                        <asp:ListItem Value="1000 ">1000 ~5sn</asp:ListItem>
                        <asp:ListItem Value="750">750 ~2sn</asp:ListItem>
                        <asp:ListItem Value="500">500 - Hızlı</asp:ListItem>
                        <asp:ListItem Value="250" Selected="True">250 - En Hızlı</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Button ID="dataekle" runat="server" onclick="dataekle_Click" 
                        Text="Depremleri Göster" />
                </td>
            </tr>
        </table>
        </asp:Panel>
      <asp:Panel ID="iPanel2" runat="server" Visible="False">
        <table align="center" class="kduzyaziki">
          <tr>
            <td align="center" valign="middle">
                &nbsp;<asp:Button ID="Adim1" runat="server" 
                    onclick="Adim1_Click" Text="&lt; Geri" Width="120px" 
                    EnableViewState="False" UseSubmitBehavior="False" 
                    CausesValidation="False" />
                &nbsp;<asp:Button ID="Button1" runat="server" Text="Tekrar Alan Seç" 
                    onclick="Button1_Click" CausesValidation="False" />
                &nbsp;<asp:Button ID="Adim2" runat="server" Height="25px" onclick="Adim2_Click" 
                    Text="İleri &gt;" Width="120px" />
                <br />
              </td>
          </tr>
            <tr>
                <td align="center" valign="middle">
                    &nbsp;</td>
            </tr>
        </table>
        </asp:Panel>
    </div>
  </div>
    <div id="baslik" style="width: 100%; text-align:center" align="center"> 
      <textarea runat="server" id="tracedata" cols="20" rows="1" name="S2"></textarea><textarea 
            runat="server" id="maxmin" name="S1" cols="20" rows="1"></textarea><asp:RequiredFieldValidator 
            ID="maxminValidator" runat="server" 
                    ControlToValidate="maxmin"  
            ErrorMessage="Haritada bir bölge seçmelisiniz! Bu işlem için harita üzerine tıklayarak, bir alan oluşturacak şekilde noktalar yerleştirebilirsiniz..." 
            EnableTheming="True">*</asp:RequiredFieldValidator>            </div> 
  <div id="taban">
        Ankara Üniversitesi Mühendislik Fakültesi     Jeofizik Mühendisliği Bölümü<br />
        Mezuniyet Tezi I&nbsp; (2009)<br />
        Mehmet DURMAZ 01201529<br />
    Danışman: Yrd.Doç.Dr. Bülent Kaypak<br />
        DEPREM RİSK ANALİZİ YAPAN ÇEVİRİM İÇİ WEB UYGULAMASI<br />
        UYARI: SİTEDEN ALDIĞINIZ SONUÇLARI VEYA GÖRSELLERİ AKADEMİK ALANDA 
        KULLANACAĞINIZDA<br />
        TEZ KONUSUNA ve İSİMLERE ATIF YAPMANIZ BEKLENİR.</div>
  </form>
</body>
</html>
