
/**
 * 中心医疗机构字典表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.clientele.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
    ],
    title:'中心医疗机构信息',
    width:640,
	height:450,
	bodyPadding:'10px 5px 0px 10px',
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddCLIENTELE',
    /**修改服务地址*/
    editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField', 
    /**获取数据服务路径*/
	selectUrl2: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	border:true,
    layout:{
        type:'table',
        columns:2//每行有几列
    },
	/**每个组件的默认属性*/
	defaults: {
        width:220,
		labelWidth: 100,
		labelAlign: 'right'
	},
	formtype:'edit',
	AreaID:null,
	AreaEnum:{},
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [{
			fieldLabel:'编号',name:'CLIENTELE_Id',itemId:'CLIENTELE_Id',
			regex: /^[0-9]\d*$/,regexText : '只能输入数字',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'客户中文名称',name:'CLIENTELE_CNAME',
			emptyText:'必填项',allowBlank:false, colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel: '客户英文名称',name: 'CLIENTELE_ENAME',
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'输入代码',name:'CLIENTELE_SHORTCODE',colspan:1,
		     emptyText:'必填项',allowBlank:false,width: me.defaults.width * 1
		}, {
			fieldLabel:'联系人',
			name:'CLIENTELE_LINKMAN',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'联系人职务',name:'CLIENTELE_LinkManPosition',colspan:1,
			width: me.defaults.width * 1,itemId:'CLIENTELE_Price'
		},{
			fieldLabel:'联系方式1',name:'CLIENTELE_PHONENUM1',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'联系方式2',name:'CLIENTELE_PHONENUM2',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'地址',name:'CLIENTELE_ADDRESS',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'EMAIL',name:'CLIENTELE_EMAIL',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'办事处',name:'CLIENTELE_Groupname',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'邮编',name:'CLIENTELE_MAILNO',
			regex: /^[1-9]\d{5}(?!\d)$/,regexText : '请输入正确的邮政编码',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'业务员',name:'CLIENTELE_Bmanno',
			colspan:1,width: me.defaults.width * 1
		}, {
			fieldLabel:'负责人',name:'CLIENTELE_PRINCIPAL',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'传真号码',name:'CLIENTELE_FaxNo',
			regex: /^[0-9]\d*$/,regexText : '只能输入数字',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自动传真号码',name:'CLIENTELE_AutoFax',
			regex: /^[0-9]\d*$/,regexText : '只能输入数字',colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义1',name:'CLIENTELE_CZDY1',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义2',name:'CLIENTELE_CZDY2',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义3',name:'CLIENTELE_CZDY3',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义4',name:'CLIENTELE_CZDY4',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义5',name:'CLIENTELE_CZDY5',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'自定义6',name:'CLIENTELE_CZDY6',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel: '区域',name: 'CLIENTELE_AreaName',itemId: 'CLIENTELE_AreaName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.weixin.hospital.area.CheckGrid',
			classConfig: {
				title: '区域选择',checkOne:true,
				width:300
			},
			listeners: {
				check: function(p, record) {
					me.onAreaAccept(record);
					p.close();
				}
			}
		},{
			fieldLabel: '区域ID',hidden: true,name: 'CLIENTELE_AreaID',itemId: 'CLIENTELE_AreaID'
		},{
			fieldLabel:'关联名称',name:'CLIENTELE_RelationName',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'区域医疗机构编码',name:'CLIENTELE_WebLisSourceOrgID',
			colspan:1,width: me.defaults.width * 1
		},{
			fieldLabel:'是否使用',name:'CLIENTELE_ISUSE',
			xtype: 'uxBoolComboBox',value: true
		},{
			fieldLabel:'备注',height:50,//labelAlign:'top',
			name:'CLIENTELE_Romark',xtype:'textarea',
			colspan:2,width: me.defaults.width * 2
		}];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CNAME:values.CLIENTELE_CNAME,
			ENAME:values.CLIENTELE_ENAME,
			SHORTCODE:values.CLIENTELE_SHORTCODE,
			LINKMAN:values.CLIENTELE_LINKMAN,
			LinkManPosition:values.CLIENTELE_LinkManPosition,
			PHONENUM1:values.CLIENTELE_PHONENUM1,
			PHONENUM2:values.CLIENTELE_PHONENUM2,
			ADDRESS:values.CLIENTELE_ADDRESS,
			EMAIL:values.CLIENTELE_EMAIL,
			MAILNO:values.CLIENTELE_MAILNO,
			PRINCIPAL:values.CLIENTELE_PRINCIPAL,
			FaxNo:values.CLIENTELE_FaxNo,
			CZDY1:values.CLIENTELE_CZDY1,
			CZDY2:values.CLIENTELE_CZDY2,
			CZDY3:values.CLIENTELE_CZDY3,
			CZDY4:values.CLIENTELE_CZDY4,
			CZDY5:values.CLIENTELE_CZDY5,
			CZDY6:values.CLIENTELE_CZDY6,
			RelationName:values.CLIENTELE_RelationName,
			WebLisSourceOrgID:values.CLIENTELE_WebLisSourceOrgID,
			Romark:values.CLIENTELE_Romark,
			ISUSE:values.CLIENTELE_ISUSE ? 1 : 0
		};
		if(values.CLIENTELE_Groupname){
			entity.Groupname=values.CLIENTELE_Groupname;
		}
		if(values.CLIENTELE_Id){
			entity.Id=values.CLIENTELE_Id;
		}
		if(values.CLIENTELE_AreaID){
			entity.AreaID=values.CLIENTELE_AreaID;
		}
		if(values.CLIENTELE_AutoFax){
			entity.AutoFax=values.CLIENTELE_AutoFax;
		}
		if(values.CLIENTELE_Bmanno){
			entity.Bmanno=values.CLIENTELE_Bmanno;
		}
	    return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams(),
			fieldsArr = [];
		var fields = ['Id','CNAME','ADDRESS','AreaID','AutoFax',
		'Bmanno','CZDY1','CZDY2','CZDY3','CZDY4','CZDY5','CZDY6',
		'EMAIL','ENAME','FaxNo','ISUSE','LINKMAN','LinkManPosition',
		'MAILNO','PHONENUM1','PHONENUM2','PRINCIPAL','RelationName',
		'Romark','SHORTCODE','WebLisSourceOrgID','Groupname'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.CLIENTELE_Id;
		return entity;
	},

	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		var me =this;
	    data.CLIENTELE_ISUSE = data.CLIENTELE_ISUSE=='1' ? true :false;
      
		var Name = me.getComponent('CLIENTELE_AreaName');
		var AreaName='';
		//区域
		if(data.CLIENTELE_AreaID){
            if(me.AreaEnum != null){
				AreaName = me.AreaEnum[data.CLIENTELE_AreaID];
			}
		}
		Name.setValue(AreaName);
		return data;
	},
	isEdit:function(id){
		var me = this;
		
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.setReadOnly(false);
		
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.load(id);
		var Id = me.getComponent('CLIENTELE_Id');
	    Id.setReadOnly(true);
	},
	
	isAdd:function(AreaID,AreaName){
		var me = this;
		
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.setReadOnly(false);
		
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
		
		var Id = me.getComponent('CLIENTELE_AreaID');
		var Name = me.getComponent('CLIENTELE_AreaName');
	    Id.setValue(AreaID);
	    Name.setValue(AreaName);
	},
	
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				
				if(me.isReturnData){
					me.fireEvent('save',me,me.returnData(id));
				}else{
					me.fireEvent('save',me,id);
				}
				
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	changeTitle:function(){
		
	},
	/**区域选择*/
	onAreaAccept: function(record) {
		var me = this;
		var AreaID = me.getComponent('CLIENTELE_AreaID');
		var AreaName = me.getComponent('CLIENTELE_AreaName');

		AreaID.setValue(record ? record.get('ClientEleArea_Id') : '');
		AreaName.setValue(record ? record.get('ClientEleArea_AreaCName') : '');
	},
    /**校验是否存在相同的编号*/
	getisValidItemNo:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT +me.selectUrl2;
		var values = me.getForm().getValues();		
		url += "&fields=CLIENTELE_Id";
		var where="&where=clientele.Id="+values.CLIENTELE_Id +"";
//		if(values.CLIENTELE_Id=){
//			where+=" and clientele.Id!="+values.CLIENTELE_Id;
//		}
		url +=where;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
    /**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		if(!me.getForm().isValid()) return;
		var isExec=true;
		if(me.formtype=='add'){
			//校验项目编号
	        me.getisValidItemNo(function(data){
	        	if(data && data.value){
	        		var len = data.value.list.length;
	        		if(len>0){
	        			var ItemNo=data.value.list[0].CLIENTELE_Id;
	        			isExec=false;
	        			JShell.Msg.error('编号:【'+ItemNo+'】已存在！');
	        		}
	        	}
	        });
		}
        if(!isExec) return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				
				if(me.isReturnData){
					me.fireEvent('save',me,me.returnData(id));
				}else{
					me.fireEvent('save',me,id);
				}
				
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	}
	
});