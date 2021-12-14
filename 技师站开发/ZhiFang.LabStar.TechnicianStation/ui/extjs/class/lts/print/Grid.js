/**
 * 打印样本清单
 * @author liangyl
 * @version 2019-12-06
 */
Ext.define('Shell.class.lts.print.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '打印样本清单',
	requires: ['Ext.ux.CheckColumn','Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger','Shell.ux.form.field.SimpleComboBox'],
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormAndItemNameList?isPlanish=true',
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:5000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**小组*/
	SectionID: null,
		
	//默认排序字段
	defaultOrderBy:[
		{property:'LisTestForm_GTestDate',direction:'ASC'},
		{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
	],
	/**检验状态列表*/
	mainStatusList:[
	    ['-2', '作废'],
		['0', '检验中'],
		['2', '检验确认'],
		['3', '检验审核']
    ],
	/**检验状态默认值*/
    defaultMainStatus:'0',
     /**日期范围默认值*/
    defaultAddDate:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//初始化时间
		me.initDate();
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
			isDate:true,sortable:true,menuDisabled:true,defaultRenderer:true
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
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'LisTestForm_PatNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'科室',dataIndex:'LisTestForm_LisPatient_DeptName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单项目',dataIndex:'LisTestForm_ItemNameList',minWidth:180,flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createToolbarItems());

		return items;
	},
	/**查询按钮栏*/
	createToolbarItems: function() {
		var me = this,
			items = ['refresh',{
			xtype: 'uxdatearea',itemId: 'date',labelWidth: 60,labelAlign: 'right',
			fieldLabel: '日期范围',value:me.defaultAddDate,
			listeners: {
				enter: function() {
					me.onSearch(); 
				}
			}
		},'-',{
            xtype: 'textfield',itemId: 'StartSampleNo', fieldLabel: '样本号范围',emptyText: '开始样本号',labelAlign:'right',width:160,labelWidth:70
        },{
        	xtype: 'textfield',itemId:'EndSampleNo',emptyText: '结束样本号',fieldLabel:'-',labelSeparator:'',labelWidth:10,width:100
        }, {
		   width: 150,labelWidth: 60,labelAlign: 'right',xtype: 'uxSimpleComboBox',itemId: 'MainStatusID',
		   fieldLabel: '检验状态',data: me.mainStatusList,value: me.defaultMainStatus
	    },{text:'查询',tooltip:'查询',iconCls:'button-search',
		    handler:function(but,e){
		    	me.onSearch();
		    }
		}];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'searchToolbar',
			items: items
		});
	},
	
		/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			text:'打印',tooltip:'打印',iconCls:'button-print',handler: function (but, e) {
				JShell.Win.open('Ext.panel.Panel', {
					title: '打印',width: '400px',height: '340px',
					listeners: {
						show: function (t) {
							var iframe = "<iframe id='printIframe' name='printIframe' width='100%' height='100%' src=" + JShell.System.Path.ROOT + "/ui/layui/views/system/comm/template/print/index.html?BusinessType=1&ModelType=1></iframe>";
							t.update(iframe);
							setTimeout(function () {
								window.frames["printIframe"].frameElement.contentWindow.PrintDataStr = me.getPrintData();//传递JSON数据参数
							}, 200);
						}
					}
				}).show();
         	}
		},{
			text:'设计模板',tooltip:'设计模板',margin:'0 0 0 10',iconCls:'button-config',
				handler: function (but, e) {
					JShell.Win.open('Ext.panel.Panel', {
						title:'设计模板',
						width: '95%',
						height: '80%',
						listeners: {
							show: function (t) {
								var iframe = "<iframe width='100%' height='100%' src=" + JShell.System.Path.ROOT + "/ui/layui/views/system/comm/template/index.html?type=2&BusinessType=1&ModelType=1" + "></iframe>";
								t.update(iframe);
							}
						}
					}).show();
	    	}
		},{
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '预览',
            name: 'showpdf',itemId:'showpdf',margin:'0 0 0 100',
            checked:true,//labelSeparator:'',
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        });
		return buttonToolbarItems;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		var Obj = me.searchObj();
		//样本号范围
		if( Obj.StartSampleNo)url+='&beginSampleNo='+ Obj.StartSampleNo;
		if( Obj.EndSampleNo)url+='&endSampleNo='+ Obj.EndSampleNo;
		//小组Id
		if(me.SectionID)params.push("listestform.LBSection.Id=" + me.SectionID + "");
        //状态
		if(Obj.MainStatusID)params.push("listestform.MainStatusID=" + Obj.MainStatusID + "");
		//时间范围
		if( Obj.BeginDate)params.push("listestform.GTestDate>='" + JShell.Date.toString( Obj.BeginDate,true) + "'");
		if( Obj.EndDate)params.push("listestform.GTestDate<'" + JShell.Date.toString(JShell.Date.getNextDate( Obj.EndDate),true) + "'");
		
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
    //查询条件数据
   searchObj:function(){
   	    var me = this,
		    searchToolbar = me.getComponent('searchToolbar'),
		    DateArea = searchToolbar.getComponent('date'),
    	    date = DateArea.getValue(),
    	    MainStatusID = searchToolbar.getComponent('MainStatusID').getValue(),
            StartSampleNo = searchToolbar.getComponent('StartSampleNo').getValue(),
    	    EndSampleNo = searchToolbar.getComponent('EndSampleNo').getValue();

        return {
			BeginDate: JShell.Date.toString(date.start),
			EndDate: JShell.Date.toString(date.end),
			EndSampleNo:EndSampleNo,
			StartSampleNo:StartSampleNo,
			MainStatusID:MainStatusID
		};
   },
   /**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
		var dateArea = {
			start: Sysdate,
			end: Sysdate
		};
		me.defaultAddDate = dateArea;
	},
	/**默认选中处理*/
	doAutoSelect: function(records, autoSelect) {

		if (!records || records.length <= 0) {
			return;
		}

		var me = this,
			len = records.length - 1;

		if (len < 0) return;

		if (autoSelect === false) return;

		var type = Ext.typeOf(autoSelect),
			num = autoSelect === true ? 0 : -1;

		if (type === 'string') { //需要选中的行主键
			num = me.store.find(me.PKField, autoSelect);
		}

		//选中行号为num的数据行
		if (num >= 0) {
			me.getSelectionModel().selectAll();
		}
	},
	//获取打印选择行
	getPrintData : function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var list = [],data=[[]];
		for (var i in records) {
			list.push(records[i].data);
		}
		if(list.length==0)return data;
		data = JSON.stringify([list]).replace(RegExp("LisTestForm_", "g"), "").replace(RegExp("LisPatient_", "g"), "");
        return data;
	}
});