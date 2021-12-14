/**
 * 产品上传面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.UploadPanel',{
    extend:'Ext.form.Panel',
    title:'产品上传面板',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    width:400,
    height:190,
    
    /**文件上传服务*/
   	url:'/ReagentService.svc/ST_UDTO_UploadGoodsDataByExcel',
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
    
    /**机构信息*/
    CenOrg:{Id:'',Name:'',readOnly:false},
    /**供应商信息*/
    Comp:{Id:'',Name:'',readOnly:false},
    /**厂商信息*/
    Prod:{Id:'',Name:'',readOnly:false},
    
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
    	//LabID，CompID，ProdID
    	
    	if(me.formType == 'Lab'){
    		items = me.createLabItems();
    	}
    	if(me.formType == 'Comp'){
    		items = me.createCompItems();
    	}
    	
		//厂商
		items.push({
			fieldLabel: '厂商',
			name: 'ProdName',
			itemId: 'ProdName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='1'",
				title:'厂商选择'
			},
			emptyText:'必填项',allowBlank:false,
			readOnly:me.Prod.readOnly,value:me.Prod.Name
		});
		
		//ID
		items.push({
			fieldLabel: '机构主键ID',
			name: 'LabID',
			itemId: 'LabID',
			xtype:'textfield',
			hidden: true,
			value:me.CenOrg.Id
		}, {
			fieldLabel: '供应商主键ID',
			name: 'CompID',
			itemId: 'CompID',
			xtype:'textfield',
			hidden: true,
			value:me.Comp.Id
		}, {
			fieldLabel: '厂商主键ID',
			name: 'ProdID',
			itemId: 'ProdID',
			xtype:'textfield',
			hidden: true,
			value:me.Prod.Id
		});
		
 		//文件
    	items.push({
    		xtype:'filefield',allowBlank:false,emptyText:me.fileEmptyText,
    		buttonConfig:{iconCls:'button-search',text:'选择'},
			name:'File',itemId:'File',fieldLabel: '产品文件'
    	});
    	
    	return items;
    },
    createLabItems:function(){
    	var me = this,
    		items = [];
    		
		//实验室
		items.push({
			fieldLabel: '实验室',
			name: 'LabName',
			itemId: 'LabName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='3'",
				title:'实验室选择'
			},
			emptyText:'必填项',allowBlank:false,
			readOnly:me.CenOrg.readOnly,value:me.CenOrg.Name
		});
		//供应商
		items.push({
			fieldLabel: '供应商',
			name: 'CompName',
			itemId: 'CompName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='2' or cenorg.CenOrgType.ShortCode='1'",
				title:'供应商选择'
			},
			emptyText:'必填项',allowBlank:false,
			readOnly:me.Comp.readOnly,value:me.Comp.Name
		});
    	
    	return items;
    },
    createCompItems:function(){
    	var me = this,
    		items = [];
    		
		//实验室
		items.push({
			fieldLabel: '供应商',
			name: 'LabName',
			itemId: 'LabName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='2' or cenorg.CenOrgType.ShortCode='1'",
				title:'供应商选择'
			},
			emptyText:'必填项',allowBlank:false,
			readOnly:me.CenOrg.readOnly,value:me.CenOrg.Name
		});
		//供应商
		items.push({
			fieldLabel: '上级供应商',
			name: 'CompName',
			itemId: 'CompName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='2' or cenorg.CenOrgType.ShortCode='1'",
				title:'上级供应商选择'
			},
			emptyText:'必填项',allowBlank:false,
			readOnly:me.Comp.readOnly,value:me.Comp.Name
		});
    	
    	return items;
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
		var me = this,
			LabName = me.getComponent('LabName'),
			LabId = me.getComponent('LabID'),
			CompName = me.getComponent('CompName'),
			CompId = me.getComponent('CompID'),
			ProdName = me.getComponent('ProdName'),
			ProdId = me.getComponent('ProdID');
		
		if(me.formType == 'Lab'){
			LabName.on({
				check: function(p, record) {
					LabName.setValue(record ? record.get('CenOrg_CName') : '');
					LabId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
			CompName.on({
				check: function(p, record) {
					CompName.setValue(record ? record.get('CenOrg_CName') : '');
					CompId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
			ProdName.on({
				check: function(p, record) {
					ProdName.setValue(record ? record.get('CenOrg_CName') : '');
					ProdId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
		}
		if(me.formType == 'Comp'){
			LabName.on({
				check: function(p, record) {
					LabName.setValue(record ? record.get('CenOrg_CName') : '');
					LabId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
			CompName.on({
				check: function(p, record) {
					CompName.setValue(record ? record.get('CenOrg_CName') : '');
					var id = record ? record.get('CenOrg_Id') : '';
					CompId.setValue(id);
					//LabId.setValue(id);
					p.close();
				}
			});
			ProdName.on({
				check: function(p, record) {
					ProdName.setValue(record ? record.get('CenOrg_CName') : '');
					ProdId.setValue(record ? record.get('CenOrg_Id') : '');
					p.close();
				}
			});
		}
		if(me.formType == 'Prod'){
			ProdName.on({
				check: function(p, record) {
					ProdName.setValue(record ? record.get('CenOrg_CName') : '');
					var id = record ? record.get('CenOrg_Id') : '';
					ProdId.setValue(id);
					CompId.setValue(id);
					LabId.setValue(id);
					p.close();
				}
			});
		}
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
        		if(me.showSuccessInfo){
        			JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
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
    		url = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_DownLoadExcel';
		
		url += '?downFileName=错误详细信息文件&operateType=0&fileName=' + fileName;
		window.open(url);
    }
});