/**
 * 字典对照
 * @author GHX
 * @version 2021-03-25
 */
var ITEMS = null;
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'layer', 'laydate', 'element', 'table', 'form'], function () {
	var $ = layui.jquery,
		layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		element = layui.element,
		laydate = layui.laydate,
		form = layui.form,
		$ = layui.jquery;
		
	var app = {
		
	};
	app.url={
		GET_ITEMINFO:uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemBySickTypeID'
	};
	/*-------初始化界面------*/
	app.init = function () {
		var  params = uxutil.params.get(false);
		console.log(params);
		var url = app.url.GET_ITEMINFO + "?t=5562185263685225"
		if (params.SectionID) {
			url +="&SectionID="+params.SectionID;
		} 
		if(params.GroupType){
			url +="&GroupType="+params.GroupType;
		}
		if(params.SickTypeID){
			url +="&SickTypeID="+params.SickTypeID;
		}
		if(params.ItemCName){
			url +="&ItemCName="+params.ItemCName;
		}
		app.getItemInfo(url);
	};	
	//获得质控项目下拉框信息
	app.getItemInfo = function (url) {
		var me = this;
		uxutil.server.ajax({ url: url }, function (res) {
			if (res.success && res.ResultDataValue) {
				var value = res.value,
					html = "";
				ITEMS = value;	
				$.each(value, function (i,item) {
					html += "<option value='" + item["Id"] +"'>" + item["CName"] +"</option>";
				});
				$("#ItemID").html(html);
				form.render('select');
			}
		});
	};
	app.init();
});
