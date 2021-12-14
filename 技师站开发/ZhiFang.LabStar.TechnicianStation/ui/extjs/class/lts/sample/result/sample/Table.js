/**
 * 项目表格
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.Table', {
	extend:'Ext.panel.Panel',
	bodyPadding:1,
	title:'项目表格',
	
	//批量更新样本项目结果
	editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_EditBatchLisTestItemResult',
	
	//每列最大行数
	MAX_NUMBER:10,
	//列数
	COL_NUMBER:1,
	//卡片宽度
	CARD_WIDTH:370,
	//卡片高度
	CARD_HEIGHT:25,

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	//创建卡片面板
	createCards:function(data){
		var me = this,
			list = data || [],
			len = list.length,
			items = [];
		
		for(var i=0;i<len;i++){
			items.push({
				xtype:'panel',margin:1,
				width:me.CARD_WIDTH,
				height:me.CARD_HEIGHT,
				html:me.createCell(list[i],i)
			});
		}
		//me.onLayoutChangeByItems(items);
		me.removeAll();
		me.add(items);
	},
	//创建单元格
	createCell:function(data,index){
		var me = this,		
			html = me.getCellTemplet();
		
		html = html.replace(/{NO}/g,index+1);//序号
		html = html.replace(/{ItemName}/g,data.LisTestItem_LBItem_CName);//项目名称
		html = html.replace(/{PreValue}/g,data.LisTestItem_PreValue);//上次结果
		html = html.replace(/{ReportValue}/g,data.LisTestItem_ReportValue);//报告值
		html = html.replace(/{EResultStatus}/g,data.LisTestItem_EResultStatus);//结果状态
		html = html.replace(/{OriglValue}/g,data.LisTestItem_OriglValue);//原始值
		html = html.replace(/{Unit}/g,data.LisTestItem_Unit);//结果单位
		html = html.replace(/{RefRange}/g,data.LisTestItem_RefRange);//参考范围
		
		return html;
	},
	//获取单元格模板
	getCellTemplet:function(){
		var me = this; 
		var html = [];
		
		html.push('<div style="padding:2px; width:100%;white-space:nowrap;overflow:auto;">');
		html.push('<div style="float:left;padding-left:5px;width:">{ItemName}</div>');//项目名称
		html.push('<div style="float:left;padding-left:5px;">{PreValue}</div>');//上次结果
		html.push('<input style="float:left;padding-left:5px;" value="{ReportValue}"></input>');//报告值
		html.push('<div style="float:left;padding-left:5px;">{EResultStatus}</div>');//结果状态
		html.push('<div style="float:left;padding-left:5px;">{OriglValue}</div>');//原始值
		html.push('<div style="float:left;padding-left:5px;">{Unit}</div>');//结果单位
		html.push('<div style="float:left;padding-left:5px;">{RefRange}</div>');//参考范围
		html.push('</div>');
		
		return html.join('');
	},
	//更新数据
	onLoadByData:function(data){
		this.createCards(data);
	},
	//删除项目
	onDelClick:function(){
		var me = this;
		JShell.Msg.overwrite('Shell.class.lts.sample.result.sample.Table:onDelClick');
	},
	//清空数据,禁用功能按钮
	clearData:function(){
		var me = this;
		
		me.update();
	},
	onSaveClick: function (testFormRecord) {
		if (!testFormRecord) return;
		var me = this,
			lisTestFormID = testFormRecord.get("LisTestForm_Id"),
			list = [];
		
		return;
			
		me.showMask(me.saveText);//显示遮罩层
		var url = JShell.System.Path.ROOT + me.editUrl;
		JShell.Server.post(url, Ext.JSON.encode({
			testFormID: lisTestFormID,
			listTestItemResult:list
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				JShell.Msg.alert("保存成功！",null,1000);
				me.fireEvent('aftersave',me);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});