/**
 * 公共css代码
 * @type String
 */
var css =  
    "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/icon.css'/>" +  
    "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/main.css'/>" +  
    "<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/style.css'/>" +  
"<link rel='stylesheet' type='text/css' href='" + getRootPath() + "/ui/css/buttons.css'/>" +  
	"<style type='text/css'>" + 
		".x-grid-row{vertical-align:middle}" + 
		".x-grid-row-over .x-grid-cell-inner{font-weight:bold;}" + 
	"</style>"; 
/**
 * 公共javascript代码
 * @type String
 */
var javascript = "";
	"<script type='text/javascript'>" + 
    	
    "</script>";

var str = "<html><head></head>" + css + javascript + "<body></body></html>";

//写入当前页面中
document.write(str);
