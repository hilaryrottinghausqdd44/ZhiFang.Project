/**
 * 互动列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.umeditor.List', {
	extend:'Shell.ux.grid.Panel',
	requires:[
	    'Ext.ux.RowExpander'
    ],
    
	title:'互动列表 ',
	width:800,
	height:500,
	/**默认展开内容*/
	defaultShowContent:true,
	
  	/**获取数据服务路径*/
	selectUrl:'',
	/**附件对象名*/
	objectName:'',
	/**附件关联对象名*/
	fObejctName:'',
	/**附件关联对象主键*/
	fObjectValue:'',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID:false,
  	
	/**默认加载*/
	defaultLoad:false,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用序号列*/
	hasRownumberer:false,
	
	/**默认每页数量*/
	defaultPageSize:100,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[10,10],[20,20],[50,50],[100,100],[200,200]
	],
	constructor:function(config){
		var me = this;
		
		me.plugins = [{
			ptype:'rowexpander',
			rowBodyTpl :[
				'<p><b>内容:</b></p>',
				'<p>{' + config.objectName + '_Contents}</p>'
			]
		}];
		
		me.callParent(arguments);
	},
	afterRender:function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			load:function(){
				me.changeShowType(me.showType);
			},
			resize:function(){
				var gridWidth = me.getWidth();
				var width = gridWidth - me.columns[0].getWidth() - me.columns[1].getWidth() - me.columns[3].getWidth();
				me.columns[2].setWidth(width - 20);
			}
		});
	},
	initComponent:function() {
		var me = this;
		
		me.objectNameLower = me.objectName.toLocaleLowerCase();
		
		if(me.defaultWhere){
			me.defaultWhere = "(" + me.defaultWhere + ") and ";
		}else{
			me.defaultWhere = "";
		}
		
//		me.defaultWhere += me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + 
//			me.fObejctName + ".Id=" + me.fObjectValue;
			
		/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
		if(me.fObjectIsID){
			me.defaultWhere += me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + 
				me.fObejctName + "ID=" + me.fObjectValue;
		}else{
			me.defaultWhere += me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + 
				me.fObejctName + ".Id=" + me.fObjectValue;
		}
		
			
		me.columns = me.createGridColumns();
		
		me.buttonToolbarItems = ['refresh',{
			xtype:'checkbox',
			boxLabel:'展开内容',
			itemId:'showContent',
			checked:me.defaultShowContent,
			listeners:{
				change:function(field,newValue,oldValue){
					me.changeShowType(newValue);
				}
			}
		},{
			width:165,labelWidth:70,labelAlign:'right',fieldLabel:'发表时间',
			itemId:'BeginDate',xtype:'datefield',format:'Y-m-d'
		},{
			width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
			itemId:'EndDate',xtype:'datefield',format:'Y-m-d'
		}];
		
		me.showType = me.defaultShowContent;
		me.defaultOrderBy = [{property:me.objectName + '_DataAddTime',direction:'DESC'}];
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'发表时间',dataIndex:me.objectName + '_DataAddTime',isDate:true,hasTime:true,width:155
		},{
			text:'发表人',width:200,dataIndex:me.objectName + '_SenderName',
			sortable:false,menuDisabled:true,
			renderer:function(value,meta,record){
				var isOwner = record.get(me.objectName + '_SenderID') == 
					JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				var color = isOwner ? "color:green" :"color:#000";
				var v = '<b style=\'' + color + '\'>' + record.get(me.objectName + '_SenderName') + '</b>';
				if (v) {
					meta.tdAttr = 'data-qtip="' + v + '"';
				}
				return v;
			}
		},{
			xtype:'rownumberer',
			text:'序号',
			width:40,
			align:'center'
		},{
			text:'主键ID',isKey:true,hidden:true,hideable:false,dataIndex:me.objectName + '_Id'
		},{
			text:'发表人ID',dataIndex:me.objectName + '_SenderID',notShow:true
		},{
			text:'内容',dataIndex:me.objectName + '_Contents',notShow:true
		}];
		
		return columns;
	},
	changeShowType:function(value){
		var me = this;
		me.showType = value ? true :false;
		me.toggleRow(me.showType);
	},
	toggleRow:function(bo){
		var me = this,
			plugins = me.plugins[0],
        	view = plugins.view,
			records = me.store.data,
			len = records.length;
        	
        for(var i=0;i<len;i++){
			var rowNode = view.getNode(i),
	            row = Ext.get(rowNode),
	            nextBd = Ext.get(row).down(plugins.rowBodyTrSelector),
	            record = view.getRecord(rowNode);
	        if(bo){
	        	row.removeCls(plugins.rowCollapsedCls);
	            nextBd.removeCls(plugins.rowBodyHiddenCls);
	            plugins.recordsExpanded[record.internalId] = true;
	        }else{
	        	row.addCls(plugins.rowCollapsedCls);
	            nextBd.addCls(plugins.rowBodyHiddenCls);
	            plugins.recordsExpanded[record.internalId] = false;
	        }
		}
        view.refreshSize();
        if(bo){
            view.fireEvent('expandbody', rowNode, record, nextBd.dom);
        }else{
            view.fireEvent('collapsebody', rowNode, record, nextBd.dom);
        }
    },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			params = [];
		
		if(BeginDate){
			params.push(me.objectNameLower + ".DataAddTime>='" + 
				JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push(me.objectNameLower + ".DataAddTime<'" + 
				JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		return me.callParent(arguments);
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		
//		var reg = new RegExp("<br />", "g");
//		var reg2 = new RegExp("&#92", "g");
//		data.FFile_Memo = data.FFile_Memo.replace(reg, "\r\n");		
//		data.FFile_Memo = data.FFile_Memo.replace(reg2, '\\');
		return data;
	}
});