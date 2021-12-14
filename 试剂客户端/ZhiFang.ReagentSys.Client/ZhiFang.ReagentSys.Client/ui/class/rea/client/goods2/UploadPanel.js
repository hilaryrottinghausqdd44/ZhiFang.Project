/**
 * 货品上传面板
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.client.goods2.UploadPanel',{
    extend:'Ext.form.Panel',
    title:'货品上传面板',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    width:400,
    height:150,
    
    /**文件上传服务*/
   	url:'/ReaManageService.svc/RS_UDTO_UploadReaGoodsDataByExcel',
   	/**空白提示*/
    fileEmptyText:'Excel格式文件',
    /**显示成功信息*/
	showSuccessInfo:true,
	
	/**导入类型*/
	formType:'',
	
	ERROR_UNIQUE_KEY_INFO:JShell.Server.Status.ERROR_UNIQUE_KEY,
	
	bodyPadding:10,
    layout:'anchor',
    /**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
    	labelWidth:60,
    	labelAlign:'right',
    },
    /*厂商*/
	ProdOrg: 'ProdOrg',
	  /**机构信息*/
    CenOrg:{Id:'',Name:'',readOnly:false},
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	//初始化检索监听
		me.initFilterListeners();
    },
    initComponent:function(){
    	var me = this;
    	me.addEvents('save');
    	
    	if(me.formType == 'Comp'){
    		me.defaults.labelWidth = 70;
    	}else if(me.formType == 'Prod'){
    		me.height = me.height - 60;
    	}
    	
    	me.items = me.items || me.createItems();
    	me.dockedItems = me.dockedItems || me.createDockedItems();
    	
    	me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems:function(){
    	var me = this,
    		items = [];
		//厂商
//		items.push({
//			fieldLabel: '厂商',
//			name: 'ProdName',
//			itemId: 'ProdName',
//			xtype: 'uxCheckTrigger',
//			className: 'Shell.class.wfm.dict.CheckGrid',
//			classConfig: {
//				title: '厂商选择',
//				defaultWhere: "bdict.BDictType.DictTypeCode='" + this.ProdOrg + "'"
//			},
//          listeners: {
//				check: function(p, record) {
//					me.onCompAccept(p, record);
//				}
//			}
//		});
		
		items.push({
			fieldLabel: '实验室',
			name: 'LabName',hidden:true,
			itemId: 'LabName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='3'",
				title:'实验室选择'
			},
//			emptyText:'必填项',allowBlank:false,
			readOnly:me.CenOrg.readOnly,value:me.CenOrg.Name
		});
		
		//ID
		items.push({
			fieldLabel: '机构主键ID',
			name: 'LabID',
			itemId: 'LabID',
			xtype:'textfield',
			hidden: true,
			value:me.CenOrg.Id
		});
 		//文件
    	items.push({
    		xtype:'filefield',allowBlank:false,emptyText:me.fileEmptyText,
    		buttonConfig:{iconCls:'button-search',text:'选择'},
			name:'File',itemId:'File',fieldLabel: '货品文件'
    	});
    	return items;
    },
    onCompAccept: function(p,record) {
		var me = this;
		var Id = me.getComponent('ProdID');
		var CName = me.getComponent('ProdName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
	},
	onAccept: function(p,record) {
		var me = this;
		var Id = me.getComponent('ProdID');
		var CName = me.getComponent('ProdName');
		Id.setValue(record ? record.get('ReaCenOrg_Id') : '');
		CName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		p.close();
	},
    /**创建挂靠*/
    createDockedItems:function(){
    	var me = this,
    		dockedItems = [];
		
		dockedItems.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			items:['->','accept','cancel']
		}));
    		
    	return dockedItems;
   	},
   	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	
	},
   	/**点击取消按钮处理*/
    onCancelClick:function(){
    	this.close();
    },
   	/**点击确定按钮处理*/
    onAcceptClick:function(){
    	var me = this;
		if (!me.getForm().isValid()) return;
		
		var values = me.getForm().getValues();
		
		var url = (me.url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.url;
        me.getForm().submit({
            url:url,
            waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	var msg = action.result.ErrorInfo;
        		if(me.showSuccessInfo){
        			JShell.Msg.confirm({
						icon:Ext.Msg.INFO,
						msg:msg+',如需查看详细信息请下载文件',
					},function(btn){
						if(btn != 'ok') return;
						//下载错误文件
						me.downloadErrorFile(action.result.ResultDataValue);
					});
        		}
        		me.fireEvent('save',me);
            	
            },
            failure:function(form,action){
				var msg = action.result.ErrorInfo;
				//上传的文件内容错误
				if(msg == 'Error001'){
					JShell.Msg.confirm({
						icon:Ext.Msg.ERROR,
						msg:'上传的文件内容存在问题，是否下载错误详细信息文件？'
					},function(btn){
						if(btn != 'ok') return;
						//下载错误文件
						me.downloadErrorFile(action.result.ResultDataValue);
					});
				}else{
					msg = msg ? msg : '文件上传失败！';
					JShell.Msg.error(msg);
				}
			}
        });
    },
    /**下载错误详细信息文件*/
    downloadErrorFile:function(fileName){
    	var me = this,
    		url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_DownLoadExcel';
		
		url += '?downFileName=错误详细信息文件&operateType=0&fileName=' + fileName;
		window.open(url);
    }
});