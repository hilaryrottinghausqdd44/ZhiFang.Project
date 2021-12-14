$(function() {
	var params = JcallShell.Page.getParams(true);
	if(params.URL){
		$("#iframe").attr("src",params.URL);
	}
});