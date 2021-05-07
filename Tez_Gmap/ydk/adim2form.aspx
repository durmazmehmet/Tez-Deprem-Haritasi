<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adim2form.aspx.cs" Inherits="Tez_Gmap.WebForm1" %>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>01201529 Mezuniyet Tezi</title>
<link href="tablolar.css" rel="stylesheet" type="text/css" />
<style type="text/css">
v\:* {
	behavior: url(#default#VML);
}
    #magx1
    {
        width: 50px;
    }
    #magx2
    {
        width: 50px;
    }
    </style>
</head>
<body>
<form id="sismo2" runat="server">
  <table style="width: 100%;">
    <tr>
      <td colspan="2">
          <cc1:GMap ID="GMap2" runat="server" Height="300px" 
                Key="ABQIAAAAhXI5EBjoKlMJHl55ofUpKhQPT9fGZAv7sMGBTaHGVklbTjM4oxRkBsVFCyrhrPWbZ8QG80quZngnhg" 
                Width="400px" enableServerEvents="True" Language="tr" 
                BorderStyle="Solid" BorderWidth="1px"          
            ajaxUpdateProgressMessage="&lt;blink&gt;Konum bilgisi yükleniyor...&lt;/blink&gt;" 
            enableContinuousZoom="False" enableDoubleClickZoom="True" 
            enableHookMouseWheelToZoom="False" 
                    BorderColor="Black" onmarkerclick="GMap2_MarkerClick" />
      </td>
      <td><ZGW:ZEDGRAPHWEB id="ZedGraphWeb2" runat="server" RenderMode="ImageTag"
            Width="300" Height="600" EnableViewState="False" IsFontsScaled="False" 
              IsImageMap="False" OutputFormat="Jpeg">
<XAxis Cross="0" CrossAuto="True" AxisColor="Black" Title="" MinSpace="0" IsTicsBetweenLabels="True" IsZeroLine="False" IsVisible="True" Type="Linear" IsOmitMag="False" IsShowTitle="True" IsUseTenPower="False" IsPreventLabelOverlap="True">
<FontSpec Angle="0" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="True" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>

<MinorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MinorGrid>

<MajorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MajorGrid>

<MinorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MinorTic>

<MajorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MajorTic>

<Scale Min="0" Max="0" MajorStepAuto="True" MajorStep="1" MajorUnit="Day" MinorStepAuto="True" MinorStep="1" MinorUnit="Day" MinAuto="True" MaxAuto="True" MinGrace="0.1" MaxGrace="0.1" IsReverse="False" FormatAuto="True" Format="g" Align="Center" Mag="0" MagAuto="True">
<FontSpec Angle="0" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="False" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>
</Scale>
</XAxis>

<Y2Axis Cross="0" CrossAuto="True" AxisColor="Black" Title="" MinSpace="0" IsTicsBetweenLabels="True" IsZeroLine="True" IsVisible="False" Type="Linear" IsOmitMag="False" IsShowTitle="True" IsUseTenPower="False" IsPreventLabelOverlap="True">
<FontSpec Angle="0" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="True" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>

<MinorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MinorGrid>

<MajorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MajorGrid>

<MinorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MinorTic>

<MajorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MajorTic>

<Scale Min="0" Max="0" MajorStepAuto="True" MajorStep="1" MajorUnit="Day" MinorStepAuto="True" MinorStep="1" MinorUnit="Day" MinAuto="True" MaxAuto="True" MinGrace="0.1" MaxGrace="0.1" IsReverse="False" FormatAuto="True" Format="g" Align="Center" Mag="0" MagAuto="True">
<FontSpec Angle="-90" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="False" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>
</Scale>
</Y2Axis>

<FontSpec Angle="0" Size="12" Family="Tahoma" FontColor="Black" StringAlignment="Center" 
              IsBold="True" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="True" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>

<MasterPaneFill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="Solid" AlignH="Center" AlignV="Center" IsScaled="True"></MasterPaneFill>

<YAxis Cross="0" CrossAuto="True" AxisColor="Black" Title="" MinSpace="0" IsTicsBetweenLabels="True" IsZeroLine="True" IsVisible="True" Type="Linear" IsOmitMag="False" IsShowTitle="True" IsUseTenPower="False" IsPreventLabelOverlap="True">
<FontSpec Angle="-180" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="True" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>

<MinorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MinorGrid>

<MajorGrid Color="Black" IsVisible="False" DashOn="1" DashOff="5" PenWidth="1"></MajorGrid>

<MinorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MinorTic>

<MajorTic Color="Black" Size="5" IsOutside="True" IsInside="True" IsOpposite="True" PenWidth="1"></MajorTic>

<Scale Min="0" Max="0" MajorStepAuto="True" MajorStep="1" MajorUnit="Day" MinorStepAuto="True" MinorStep="1" MinorUnit="Day" MinAuto="True" MaxAuto="True" MinGrace="0.1" MaxGrace="0.1" IsReverse="False" FormatAuto="True" Format="g" Align="Center" Mag="0" MagAuto="True">
<FontSpec Angle="90" Size="14" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="False" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="None" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>
</Scale>
</YAxis>

<Legend IsVisible="True" IsHStack="True" Position="Top" IsReverse="False">
<Location Height="0" Width="0" Y="0" X="0" AlignH="Left" AlignV="Center" CoordinateFrame="ChartFraction">
<TopLeft X="0" Y="0"></TopLeft>

<BottomRight X="0" Y="0"></BottomRight>
</Location>

<FontSpec Angle="0" Size="12" Family="Arial" FontColor="Black" StringAlignment="Center" IsBold="False" IsItalic="False" IsUnderline="False">
<Border Color="Black" IsVisible="False" Width="1" InflateFactor="0"></Border>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="Solid" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>
</FontSpec>

<Fill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="Brush" AlignH="Center" AlignV="Center" IsScaled="True"></Fill>

<Border Color="Black" IsVisible="True" Width="1" InflateFactor="0"></Border>
</Legend>

<PaneFill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="Solid" AlignH="Center" AlignV="Center" IsScaled="True"></PaneFill>

<ChartFill Color="White" ColorOpacity="100" IsVisible="True" RangeMax="0" RangeMin="0" Type="Brush" AlignH="Center" AlignV="Center" IsScaled="True"></ChartFill>

<ChartBorder Color="Black" IsVisible="True" Width="1" InflateFactor="0"></ChartBorder>

<MasterPaneBorder Color="Black" IsVisible="True" Width="1" InflateFactor="0"></MasterPaneBorder>

<Margins Left="1" Top="1" Right="1" Bottom="1"></Margins>

<PaneBorder Color="Black" IsVisible="True" Width="1" InflateFactor="0"></PaneBorder>
          </ZGW:ZEDGRAPHWEB>
          <br />
        </td>
    </tr>
    <tr>
      <td colspan="2"><table cellpadding="0" cellspacing="0" class="kduzyaziki" style="width: 100%">
          <tr>
            <td width="10%"><div align="center">0 - 2.9</div></td>
            <td style="width:10%"><div align="center">3 - 3.9</div></td>
            <td style="width:10%"><div align="center">4 - 4.9</div></td>
            <td style="width:10%"><div align="center">5 - 5.9</div></td>
            <td style="width:10%"><div align="center">6 - 7.9</div></td>
            <td rowspan="2" align="center" valign="middle" style="width:40%">
                <asp:CheckBox ID="yerbilgial" runat="server" AutoPostBack="True" 
                    oncheckedchanged="yerbilgial_CheckedChanged" 
                    Text="Tıklanan deprem için yakın yer bilgisini de çek" />
                          </td>
          </tr>
          <tr>
            <td width="5%"><div align="center"> <img alt="" src="images/iki.png" style="width: 12px; height: 12px" /></div></td>
            <td><div align="center"> <img alt="" src="images/iki.png" style="width: 12px; height: 12px" /></div></td>
            <td><div align="center"> <img alt="" src="images/dort.png" style="width: 14px; height: 14px" /></div></td>
            <td><div align="center"> <img alt="" src="images/dort.png" style="width: 14px; height: 14px" /></div></td>
            <td><div align="center"> <img alt="" src="images/alti.png" style="width: 17px; height: 17px" /></div></td>
          </tr>
        </table>
  <asp:Label ID="uyarici1" runat="server"></asp:Label>
              &nbsp;</td>
      <td>
          <asp:Label ID="LQayarla" runat="server">Kesme büyüklüğünü girerek yeniden 
          hesaplayabilirsiniz:</asp:Label>
          <br />
          <asp:Label ID="LQayarla2" runat="server"></asp:Label>
          <br />
          <asp:RequiredFieldValidator ID="magx1Vld" runat="server" 
              ControlToValidate="magx1" ErrorMessage="*"></asp:RequiredFieldValidator>
          <asp:TextBox ID="magx1" runat="server"></asp:TextBox>
          &nbsp;<asp:Button ID="kisitHesap" runat="server" onclick="kisitHesap_Click" 
              Text="Hesapla" Height="26px" Width="92px" />
          &nbsp;<asp:Button ID="RWsifirla" runat="server" onclick="RWsifirla_Click" 
              Text="Sıfırla" CausesValidation="False" />
              &nbsp;</td>
    </tr>
    <tr>
      <td>Haritada gösterilecek 
          deprem sayısını değiştirebilirsiniz:</td>
      <td>
                    <asp:DropDownList ID="MarkerLimit" runat="server" Width="136px" 
              AutoPostBack="True">
                        <asp:ListItem Value="2500">2500 ~30sn</asp:ListItem>
                        <asp:ListItem Value="2000">2000 ~ 20sn</asp:ListItem>
                        <asp:ListItem Value="1500">1500 ~15sn</asp:ListItem>
                        <asp:ListItem Value="1250">1250 ~12sn</asp:ListItem>
                        <asp:ListItem Value="1000 ">1000 ~5sn</asp:ListItem>
                        <asp:ListItem Value="750">750 ~2sn</asp:ListItem>
                        <asp:ListItem Value="500">500 - Hızlı</asp:ListItem>
                        <asp:ListItem Value="250" Selected="True">250 - En Hızlı</asp:ListItem>
                    </asp:DropDownList>
                </td>
      <td>
          Başka bir bölge seçmek için :
          <asp:Button ID="basadon" runat="server" CausesValidation="False" 
              EnableTheming="False" onclick="basadon_Click" Text="En Başa Dön" />
              </td>
    </tr>
  </table>
  <div style="text-align:center">
  <hr />
  <asp:Label ID="mmaxLabel" runat="server" Font-Bold="True"></asp:Label>
      <asp:GridView ID="DSonuclar" runat="server" BackColor="#DEBA84" 
          BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
          CellSpacing="2" HorizontalAlign="Center">
          <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
          <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
          <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
      </asp:GridView>
      <br />
    </div>  
    <div id="taban">
        Ankara Üniversitesi Mühendislik Fakültesi Jeofizik Mühendisliği Bölümü<br />
        Mezuniyet Tezi I&nbsp; (2009)<br />
        Mehmet DURMAZ 01201529<br />
        Danışman: Yrd.Doç.Dr. Bülent Kaypak<br />
        DEPREM RİSK ANALİZİ YAPAN ÇEVİRİM İÇİ WEB UYGULAMASI<br />
        UYARI: SİTEDEN ALDIĞINIZ SONUÇLARI VEYA GÖRSELLERİ AKADEMİK ALANDA 
        KULLANACAĞINIZDA TEZ KONUSUNA ve İSİMLERE ATIF YAPMANIZ BEKLENİR.<br />
  </div>
</form>
</body>
</html>