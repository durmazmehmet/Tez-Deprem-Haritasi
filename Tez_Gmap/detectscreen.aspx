<script runat="server" language="C#">

	public void Page_Load(Object sender, EventArgs e){

		if (Request.QueryString["action"] != null) {
			// store the screen resolution in Session["ScreenResolution"] 
			// and redirect back to default.aspx
			Session["ScreenResolution"] = Request.QueryString["res"].ToString();
			Response.Redirect("Default.aspx");
		}
	}
	// JavaScript code below will determine the user screen resolution and
	// redirect to itself with action=set QueryString parameter 

</script>

<HTML><BODY>
<script language="javascript"> 
res = "&res="+screen.width+"x"+screen.height+"&d="+screen.colorDepth 
top.location.href="detectscreen.aspx?action=set"+res 
</script>
</BODY></HTML>