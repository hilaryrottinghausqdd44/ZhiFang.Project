/**
 * 相关医嘱报告
 * @author Jcall
 * @version 2020-03-22
 */
Ext.define('Shell.class.lts.sample.result.report.others.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'相关医嘱报告',
	
	//是否加载过数据
	hasLoaded:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//me.html = me.title;//正式功能需要注释
		me.callParent(arguments);
	},
	//查询数据
	onSearch:function(testFormRecord){
		var me = this;
		var patno = testFormRecord ? testFormRecord.get("LisTestForm_PatNo") : null;
		if(patno == "" || patno == null){
			me.body.update('<div style="text-align:center;padding:50px;color:red;">缺失病历号无法查询！</div>');
		}else{
			var reporturl = JcallShell.System.Path.LOCAL+ "/ZhiFang.ReportFormQueryPrint/ui_new/class/labStar/index.html?PATNO="+patno;
			me.body.update("<iframe src='"+reporturl+"' style='border: medium none;height:100%;width:100%;'></iframe>");
		}
		if(!me.hasLoaded){
			//相关数据变化
			//JShell.Msg.alert(me.title + '-数据变化方法');
			me.hasLoaded = true;
		}
	},
	//清空数据,禁用功能按钮
	clearData:function(){
		this.body.update('<div style="text-align:center;padding:50px;color:red;">检验信息不存在，无法查询！</div>');
	}
});