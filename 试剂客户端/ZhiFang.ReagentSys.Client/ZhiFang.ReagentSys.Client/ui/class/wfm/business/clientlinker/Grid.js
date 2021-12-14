/**
 * 联系人列表
 * @author liangyl
 * @version 2017-03-20
 */
Ext.define('Shell.class.wfm.business.clientlinker.Grid', {
    extend: 'Shell.ux.grid.Panel',
      requires: ['Ext.ux.CheckColumn'],
//	extend: 'Shell.ux.grid.IsUseGrid',
    title: '联系人列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientLinkerByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientLinkerByField',
    /**删除数据服务路径*/
    delUrl: '/SingleTableService.svc/ST_UDTO_DelPClientLinker',

    /**默认加载*/
    defaultLoad: false,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: true,
		/**是否启用保存按钮*/
	hasSave:true,
	/**客户Id*/
	PClientID:null,
	/**客户*/
	PClientName:null,
		/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	 /**是否使用字段名*/
    IsUseField:'PClientLinker_IsUse',
     /**是否使用字段的类型，bool/int，默认bool*/
    IsUseType:'bool',
    defaultOrderBy: [{ property: 'PClientLinker_DispOrder', direction: 'ASC' }],
    /**是否启用序号列*/
	hasRownumberer: true,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        
        me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                me.openEditForm(id);
            }
        });
    },
    initComponent: function () {
        var me = this;
      	//查询框信息
		me.searchInfo = {
			width:160,emptyText:'客户名称',isLike:true,itemId:'search',
			fields:['pclientlinker.Name']
		};
        //数据列
        me.columns = me.createGridColumns();
		
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '客户名称', dataIndex: 'PClientLinker_PClient_Name', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '姓名', dataIndex: 'PClientLinker_Name', width: 100,
            sortable: false, defaultRenderer: true
        },{
            text: '英文名称', dataIndex: 'PClientLinker_EName', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '性别', dataIndex: 'PClientLinker_Sex', width: 40,
            sortable: false, 
            renderer : function(value, meta, record, rowIndex, colIndex) {
            	var v ='男';
            	if(value=='2'){
            		v='女';
            	}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
        },  {
            text: '籍贯', dataIndex: 'PClientLinker_Birthplace', width: 100,
            sortable: false, defaultRenderer: true
        },{
            text: '手机', dataIndex: 'PClientLinker_PhoneNum', width: 100,
            sortable: false, defaultRenderer: true
        },{
			xtype:'checkcolumn',text:'使用',dataIndex:'PClientLinker_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		}, {
            text: '住址', dataIndex: 'PClientLinker_Address', width: 100,
            sortable: false, defaultRenderer: true
        },{
            text: '生日', dataIndex: 'PClientLinker_Brithday', width: 85,
            sortable: false, isDate: true, defaultRenderer: true
        },   {
            text: '手机2', dataIndex: 'PClientLinker_PhoneNum2', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '固定电话', dataIndex: 'PClientLinker_TelPhone', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: 'Email', dataIndex: 'PClientLinker_Email', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: 'QQ', dataIndex: 'PClientLinker_QQ', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: 'WeiXin', dataIndex: 'PClientLinker_WeiXin', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '职务', dataIndex: 'PClientLinker_Postion', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '显示次序', dataIndex: 'PClientLinker_DispOrder', width: 70,
            sortable: false, defaultRenderer: true
        },{
            text: '备注', dataIndex: 'PClientLinker_Comment', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '主键ID', dataIndex: 'PClientLinker_Id', isKey: true, hidden: true, hideable: false
        }];

        return columns;
    },
    /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请刷新列表');
    			return;
    		}
			me.load(null, true, autoSelect);
    	});
	},
	
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			params=[],
			search = null;
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		if(me.PClientID){
			//选择全部行
			if(me.PClientID!='-1'){
			    params.push("pclientlinker.PClient.Id in (" + me.PClientID + ")");
			}
		}else{
			 params.push("pclientlinker.PClient.Id =null");
		}
		
	    if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.clientlinker.Form', {
			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:'add',
			PClientID:me.PClientID,
			PClientName:me.PClientName,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
//	/**@overwrite 修改按钮点击处理方法*/
//	onEditClick:function(){
//		var me = this;
//		var records = me.getSelectionModel().getSelection();
//		if(!records || records.length != 1){
//			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
//			return;
//		}
//		var id = records[0].get(me.PKField);
//		me.openEditForm(id);
//	},
	/**打开修改页面*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.clientlinker.Form', {
			SUB_WIN_NO:'2',//内部窗口编号
			resizable: false,
			formtype:'edit',
			PK:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get(me.IsUseField);
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		
		//是否使用的类型不同处理
		if(me.IsUseType == 'int'){
			IsUse = IsUse ? "1" : "0";
		}
		var IsUseField = me.IsUseField.split('_').slice(-1) + '';
		
		var params = {};
		params.entity = {Id:id};
		params.entity[IsUseField] = IsUse;
		params.fields = 'Id,' + IsUseField;
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	}
});