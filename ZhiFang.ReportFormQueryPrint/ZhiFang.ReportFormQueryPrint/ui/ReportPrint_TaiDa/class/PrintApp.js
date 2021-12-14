/**
 * 打印应用
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint_TaiDa.class.PrintApp',{
	extend:'Shell.ReportPrint.class.PrintApp',
	requires:['Shell.ux.form.field.DateArea'],
	layout:{type:'border',regionWeights:{west:2,south:1}},
	
	toolbars:[{dock:'top',itemId:'toptoolbar',buttons:[{
		xtype:'image',height:80,width:'100%',padding:0,src:Shell.util.Path.uiPath + '/ReportPrint_TaiDa/images/logo.jpg'
	}]}],
	
	/**创建内部组件*/
	createApps:function(){
		var me = this;
		var apps = [{
			className:'Shell.ReportPrint.class.PrintList',
			itemId:'PrintList',header:false,
			width:605,region:'west',
			split:true,collapsible:true,
			hasPrint:false,
			defaultWhere:me.defaultWhere,
			internalWhere:me.internalWhere,
			defaultLoad:(me.defaultWhere ? true : false),
			defaultPageSize:20,
			/**获取数据服务路径*/
			selectUrl:'/Ashx/SelectReportPrint.ashx?Type=SelectReport&Fields=ReportFormID,SAMPLENO,SECTIONNO,CNAME,CLIENTNO,SectionType,RECEIVEDATE,RECEIVETIME,SampleType,CHECKDATE,CHECKTIME',//,ItemName',
			toolbars:[{
				dock:'top',itemId:'toptoolbar',buttons:['refresh','-',
					{xtype:'uxdatearea',itemId:'date',fieldLabel:'核收日期',value:me.dateValue},
					{xtype:'uxbutton',itemId:'search',iconCls:'button-search',tooltip:'<b>查询</b>'},'->',
					{xtype:'uxbutton',itemId:'collapse',text:'',iconCls:'button-left',tooltip:'<b>收缩面板</b>'}
				]
			}],
			listeners:{
				afterload:function(grid,records,successful){
					grid.store.sort('RECEIVEDATE','DESC');
				}
			},
			columns:[
				{xtype:'rownumberer',text:'序号',width:35,align:'center'},
				{dataIndex:'ReportFormID',text:'主键',hideable:false,hidden:true,type:'key'},
				{dataIndex:'RECEIVEDATE',text:'核收日期',width:80,sortable:true,renderer:function(v,meta){
					var value = v ? Shell.util.Date.toString(v,true) : "";
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					return value;
				},menuDisabled:true},
				{dataIndex:'RECEIVETIME',text:'核收时间',width:65,sortable:true,renderer:function(v,meta){
					var value = v ? Shell.util.Date.toString(v).substring(11,19) : "";
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					return value;
				},menuDisabled:true},
				{dataIndex:'SAMPLENO',text:'样本编号',width:80,sortable:true,menuDisabled:true},
				{dataIndex:'SampleType',text:'样本类型',width:80,sortable:true,menuDisabled:true},
				{dataIndex:'CHECKDATE',text:'报告日期',width:80,sortable:true,renderer:function(v,meta){
					var value = v ? Shell.util.Date.toString(v,true) : "";
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					return value;
				},menuDisabled:true},
				{dataIndex:'CHECKTIME',text:'报告时间',width:65,sortable:true,renderer:function(v,meta){
					var value = v ? Shell.util.Date.toString(v).substring(11,19) : "";
					meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					return value;
				},menuDisabled:true},
				//{dataIndex:'ItemName',text:'开单项目',width:100,sortable:false},
				
				{dataIndex:'SECTIONNO',text:'检验小组编号',hideable:false,hidden:true},
				{dataIndex:'CLIENTNO',text:'送检单位编码',hideable:false,hidden:true},
				{dataIndex:'SectionType',text:'小组类型',hideable:false,hidden:true}
			],
			/**刷新处理*/
			onRefreshClick:function(){
				this.onSearchClick();
			},
			/**查询处理*/
			onSearchClick:function(){
				var date = this.getComponent("toptoolbar").getComponent("date").getValue(true),
					where = [];
					
				if(date.start){
					where.push("RECEIVEDATE>='" + Shell.util.Date.toString(date.start,true) + "'");
				}
				if(date.end){
					where.push("RECEIVEDATE<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date.end),true) + "'");
				};
				this.internalWhere = where.join(" and ");
				this.load(null,true);
			}
		},{
			className:'Shell.ReportPrint.class.PrintContent',autoScroll:true,
			itemId:'PrintContent',header:false,region:'center',
			hideToptoolbar:true,bodyPadding:1,
			listeners:{
				contentChanged:function(panel){
					var tr = document.getElementById("tr_1");
					if(tr){tr.click();}
				}
			}
		},{
			className:'Shell.ReportPrint.class.PrintChart',
			itemId:'PrintChart',header:false,
			hasAverageLine:false,hasMaxAndMin:false,showLabel:true,
			defaultSubtext:'最近一个月',defaultMonthNum:1,
			height:250,region:'south',collapsed:false,
			split:true,collapsible:true
		}];
		
		return apps;
	},
	
	/**初始化列表的条件内容*/
	initListWhere:function(){
		var me = this,
			array = me.getParamsArray(),
			len = array.length,
			params = Shell.util.Path.getRequestParams(true),
			patNO = params.PATNO;
		
		//校验定义的参数是否符合要求
		me.errorInfo = [];
		
		if(!patNO){
			me.errorInfo.push("缺少参数:PATNO(病历号)未传递到本程序!");
		}
		
		//参数正确
		if(me.errorInfo.length == 0){
			me.defaultWhere = "patNO='" + patNO + "'";
			var date = new Date();
			
			var date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
			
			date = date.setMonth(date.getMonth()-19);
			date = Shell.util.Date.toString(date,true);
			
			me.internalWhere = "RECEIVEDATE>='" + date + "' and RECEIVEDATE<'" + date_e + "'";
			me.dateValue = {start:date,end:date_e};
		}
	},
	initListeners:function(){
		var me = this;
			
		//存在错误,显示错误信息
		if(me.errorInfo.length > 0){
			me.showError(me.errorInfo.join("</br>"));
		}
	}
});