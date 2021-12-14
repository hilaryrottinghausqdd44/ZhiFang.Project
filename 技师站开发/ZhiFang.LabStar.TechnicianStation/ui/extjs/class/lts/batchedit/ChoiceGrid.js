/**
 * 检验单选择(供选择的样本单)
 * @author liangyl
 * @version 2019-12-17
 */
Ext.define('Shell.class.lts.batchedit.ChoiceGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '检验单选择',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn'
	],
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?isPlanish=true',

	/**默认每页数量*/
	defaultPageSize:50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**检验日期下拉框数组*/
	DATE_LIST: [
		['0','当日'],['-1','昨日'],['-1','2天'],['-2','3天'],
		['-4','5天'],['-6','一周'],['-9','10天'],['-14','15天'],
		['-19','20天'],['-29','30天'],['-44','45天'],['-59','60天'],['-74','75天']
	],
	
	//查询检验日期默认值
	DEFAULT_DATE_TYPE_VALUE:'0',
    //查询日期默认值
	DEFAULT_DATE_VALUE:null,
	  /**小组*/
	SectionID: null,
	//已选的检验单项目,用于填充颜色
	SelectList:[],
	/**默认数据条件*/
	defaultWhere: 'listestform.MainStatusID=0',
	
	//默认排序字段
	defaultOrderBy:[
		{property:'LisTestForm_GTestDate',direction:'ASC'},
		{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
	],
	//记录日期的排序类型
	dateDirection: 'ASC',
	//小组ID
    sectionId:null,
    //第一次加载数据
    firstLoad:true,
    
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
	    me.addEvents('editclick');

		//初始化查询日期
		me.initDefaultDate();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:true,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if (v) {
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					var Tab = record.get('LisTestForm_Tab');
					if(Tab=='1')meta.style = 'background-color:#228B22;color:#ffffff;';
				}
				return v;
			}
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNoForOrder',width:80,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNo'),
					tipText = v;
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'样本号排序',dataIndex:'LisTestForm_GSampleNo',width:150,hidden:true,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNoForOrder');
				meta.tdAttr = 'data-qtip="' + v + '"';
				return v;
			}
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'性别',dataIndex:'LisTestForm_LisPatient_GenderName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验小组',dataIndex:'LisTestForm_LBSection_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'已选择标记',dataIndex:'LisTestForm_Tab',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验单来源',dataIndex:'LisTestForm_ISource',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医嘱单ID',dataIndex:'LisTestForm_LisOrderForm_Id',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			fieldLabel:'检验日期',name:'GTestDate',editable:true, 
			xtype:'uxSimpleComboBox',value:me.DEFAULT_DATE_TYPE_VALUE,hasStyle:true,
			width:120,labelWidth:60,labelAlign:'right',itemId:'GTestDate',
			data:me.DATE_LIST,
			listeners:{
				change:function(com,newValue,oldValue,eOpts){
					me.onChangeDateByDays(newValue);
					me.onSearch();
				}
			}
		},{
			xtype:'uxdatearea',itemId:'DateValue',width:200,
			labelWidth:0,labelAlign:'right',fieldLabel:'',
			value:me.DEFAULT_DATE_VALUE,
			listeners:{
				enter:function(){me.onSearch();}
			}
		},'-',{
            xtype: 'textfield',itemId: 'StartSampleNo', fieldLabel: '样本号范围',emptyText: '开始样本号',labelAlign:'right',width:160,labelWidth:70
        },{
        	xtype: 'textfield',itemId:'EndSampleNo',emptyText: '结束样本号',fieldLabel:'-',labelSeparator:'',labelWidth:10,width:100
        },{
			text:'查询',tooltip:'查询',iconCls:'button-search',
			handler:function(button,e){me.onSearch();}
		},{height:22,overCls:'',itemId: 'btn6',text:'<span style="color:#ffffff;">已选检验单</span>',tooltip:'已选检验单',style : 'background-color:#228B22;'},
		{text:'批量删除检验单',tooltip:'批量删除检验单',iconCls:'button-del',margin:'0 5 0 20',
			handler: function(){
				me.fireEvent('editclick',me);
			}
		});
		return buttonToolbarItems;
	},
	//初始化查询日期
	initDefaultDate:function(){
		var me = this;
		var sysdate = JShell.System.Date.getDate();
		me.DEFAULT_DATE_VALUE = {start:sysdate,end:sysdate};
	},
    /**时间范围计算*/
	onChangeDateByDays:function(value){
		var me = this;
		var DateValue = me.getComponent('buttonsToolbar').getComponent('DateValue');
		var sysdate = JShell.System.Date.getDate();
		var obj ={start:sysdate,end:sysdate};
		obj.start = JShell.Date.getNextDate(sysdate,value);
		DateValue.setValue(obj);
	},
		/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		//小组Id
		if(!me.SectionID) {
			JShell.Msg.alert("小组ID不能为空");
			return;	
		}
		var DateValue = me.getComponent('buttonsToolbar').getComponent('DateValue');
        if(!DateValue.isValid()) {
        	me.hideMask();
        	return;
        }
		me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		var StartSampleNo = me.getComponent('buttonsToolbar').getComponent('StartSampleNo').getValue();
        //结束样本号
		var EndSampleNo = me.getComponent('buttonsToolbar').getComponent('EndSampleNo').getValue();
		//样本号范围
		if(StartSampleNo)url+='&beginSampleNo='+StartSampleNo;
		if(EndSampleNo)url+='&endSampleNo='+EndSampleNo;
		//日期查询栏
		var DateValue = me.getComponent('buttonsToolbar').getComponent('DateValue').getValue();

		//小组ID
		if(me.SectionID)params.push("listestform.LBSection.Id=" + me.SectionID + "");
		//检验日期范围
		if(DateValue.start)params.push("listestform.GTestDate>='" + JShell.Date.toString(DateValue.start,true) + "'");
		if(DateValue.end)params.push("listestform.GTestDate<'" + JShell.Date.toString(JShell.Date.getNextDate(DateValue.end,1),true) + "'");
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var where = me.getLoadWhere(true);
		if (where) {
			url += '&where=' + where;
		}
		return url;
	},
	
	//记录已选择检验单列表数据
	getSelectList:function(list){
		var me = this;
		me.SelectList=list;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var len = me.SelectList.length;
		for(var i = 0; i < data.list.length; i++) {
			for(var j=0;j<len;j++ ){
				if(me.SelectList[j].data.LisTestForm_Id == data.list[i].LisTestForm_Id){
					data.list[i]["LisTestForm_Tab"] = '1';
					break;
				}
			}
		}
		return data;
	},
	//加载数据前
	onBeforeLoad: function (m, operation){
		var me = this,
			where = [];
		
		//排序：检验日期+样本号
		if (!me.firstLoad) {
			me.store.proxy.extraParams = {};
			var first = me.store.sorters.first();
			if(first && (first.property == 'LisTestForm_GTestDate' || first.property == 'LisTestForm_GSampleNoForOrder')){
				//me.store.sorters.removeAll(me.store.sorters.items);
				if (first.property == 'LisTestForm_GTestDate') me.dateDirection = first.direction;
				me.store.sorters.addAll([
					{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
					{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
				]);

				//点击检验日期，顺序 检验日期顺序 + 样本号顺序
				//逆序 检验日期逆序 + 样本号逆序

				//点击样本号： 顺序 检验日期(原本的排序保持) + 样本号顺序
				//逆序 检验日期(原本的排序保持) + 样本号逆序

				//设置外部自定义参数排序  -- 替换默认排序
				me.store.proxy.extraParams = {//一个带有属性值的对象，用来给每一个使用当前对象发出的请求附加额外的参数。 Session信息以及你需要在每个请求中传递的其它信息一般都会放在这里
					sort: JSON.stringify([
						{ property: 'LisTestForm_GTestDate', direction: me.dateDirection },
						{ property: 'LisTestForm_GSampleNoForOrder', direction: first.direction }
					])
				};
			}
		}else{
			me.firstLoad = false;
		}
		me.callParent(arguments);
	}
});