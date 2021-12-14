/**
 * 实验室表单
 * @author GHX
 * @version 2019-04-10
 */
Ext.define('Shell.class.weixin.clientele.new.Form',{
    extend:'Shell.ux.form.Panel',
    requires: [
		'Ext.ux.CheckColumn',
        'Shell.ux.form.field.CheckTrigger_NEW',
        'Shell.ux.form.field.SimpleComboBox'
	],
    title:'实验室表单',
    width:240,
	height:400,
	bodyPadding:10,
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true',
	/**新增服务地址*/
	addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddCLIENTELE',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField', 	
	/**获取数据服务路径*/
	selectAreaUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaById?isPlanish=true',
	/**是否启用保存按钮*/
	//hasSave:true,
	///**是否重置按钮*/
	//hasReset:true,
    //账号Id
    Account: null,
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:80,
        labelAlign:'right'
    },
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//表单监听
        me.initFromListeners();       
	},
    initComponent: function () {
        var me = this;

        me.buttonToolbarItems = [ 'save', 'reset'];

        me.callParent(arguments);
    },
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
        var items = [
            { fieldLabel: '主键ID', name: 'CLIENTELE_Id', itemId: 'CLIENTELE_Id', emptyText: '非必填可程序生成',hidden:true, disabled: true },
			{fieldLabel:'中文名',name:'CLIENTELE_CNAME',itemId:'CLIENTELE_CNAME',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'英文名',name:'CLIENTELE_ENAME'},
			{fieldLabel:'快捷码',name:'CLIENTELE_SHORTCODE'},
			{fieldLabel:'联系人',name:'CLIENTELE_LINKMAN'},
			{fieldLabel:'联系方式1',name:'CLIENTELE_PHONENUM1'},
			{fieldLabel:'联系方式2',name:'CLIENTELE_PHONENUM2'},
			{fieldLabel:'地址',name:'CLIENTELE_ADDRESS'},
			{fieldLabel:'邮编',name:'CLIENTELE_MAILNO'},
			{fieldLabel:'邮箱',name:'CLIENTELE_EMAIL'},
			{fieldLabel:'负责人',name:'CLIENTELE_PRINCIPAL'},
			{fieldLabel:'传真号',name:'CLIENTELE_FaxNo'},
			{fieldLabel:'自动传真号码',name:'CLIENTELE_AutoFax',xtype:'numberfield',allowBlank:false},
			{fieldLabel:'自定义1',name:'CLIENTELE_CZDY1'},
			{fieldLabel:'自定义2',name:'CLIENTELE_CZDY2'},
			{fieldLabel:'自定义3',name:'CLIENTELE_CZDY3'},
			{fieldLabel:'自定义4',name:'CLIENTELE_CZDY4'},
			{fieldLabel:'自定义5',name:'CLIENTELE_CZDY5'},
			{fieldLabel:'自定义6',name:'CLIENTELE_CZDY6'},
			{fieldLabel:'关联名称',name:'CLIENTELE_BusinessName'},
			{fieldLabel:'区域Id',itemId:'CLIENTELE_AreaID',name:'CLIENTELE_AreaID',hidden:true},
			{fieldLabel:'实验室区域',xtype: 'uxCheckTrigger',name:'CLIENTELE_AreaName',
				itemId: 'CLIENTELE_AreaName',emptyText:'可双击选择',allowBlank:false,
				className: 'Shell.class.weixin.clientele.new.CheckGrid',
				listeners: {
					check: function(p, record) {
						me.getComponent('CLIENTELE_AreaID').setValue(record ? record.get('ClientEleArea_Id') : '');
                        me.getComponent('CLIENTELE_AreaName').setValue(record ? record.get('ClientEleArea_AreaCName') : '');
						p.close();
					}
				}
			},
			{fieldLabel:'使用',name:'CLIENTELE_ISUSE',xtype:'checkbox',checked:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
        var entity = {
            Id: values.CLIENTELE_Id,
			CNAME:values.CLIENTELE_CNAME,
			ENAME:values.CLIENTELE_ENAME,
			SHORTCODE:values.CLIENTELE_SHORTCODE,
			LINKMAN:values.CLIENTELE_LINKMAN,
			PHONENUM1:values.CLIENTELE_PHONENUM1,
			PHONENUM2:values.CLIENTELE_PHONENUM2,
			ADDRESS:values.CLIENTELE_ADDRESS,
			MAILNO:values.CLIENTELE_MAILNO,
			EMAIL:values.CLIENTELE_EMAIL,
			PRINCIPAL:values.CLIENTELE_PRINCIPAL,
			FaxNo:values.CLIENTELE_FaxNo,
			AutoFax:values.CLIENTELE_AutoFax,
			CZDY1:values.CLIENTELE_CZDY1,
			CZDY2:values.CLIENTELE_CZDY2,
			CZDY3:values.CLIENTELE_CZDY3,
			CZDY4:values.CLIENTELE_CZDY4,
			CZDY5:values.CLIENTELE_CZDY5,
			CZDY6:values.CLIENTELE_CZDY6,
			BusinessName:values.CLIENTELE_BusinessName,
			AreaID:values.CLIENTELE_AreaID,
			ISUSE:values.CLIENTELE_ISUSE ? 1 : 0,
        };
        if (entity.Id == "") delete entity.Id;		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
            fieldsArr = [];
        
		for(var i in fields){
			if(fields[i] != "CLIENTELE_AreaName"){
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}			
		}
		
		entity.fields = fieldsArr.join(',');
        entity.entity.Id = me.getComponent("CLIENTELE_Id").value;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var CLIENTELE_CNAME = me.getComponent('CLIENTELE_CNAME');
		CLIENTELE_CNAME.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								CLIENTELE_SHORTCODE:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						CLIENTELE_SHORTCODE:""
					});
				}
			}
		});
    },
    
   
    /**保存按钮点击处理方法*/
    onSaveClick: function () {
        var me = this;
        if (!me.getForm().isValid()) return;

        var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
        url = JShell.System.Path.getRootUrl(url);

        var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

        if (!params) return;
        var id = params.entity.Id;

        params = Ext.JSON.encode(params);

        me.showMask(me.saveText);//显示遮罩层
        me.fireEvent('beforesave', me);
        JShell.Server.post(url, params, function (data) {
            me.hideMask();//隐藏遮罩层
            if (data.success) {
                id = me.formtype == 'add' ? data.value : id;
                var AreaId = me.getForm().getValues().CLIENTELE_AreaID;
                if (Ext.typeOf(id) == 'object') {
                    id = id.id;
                }

                if (me.isReturnData) {
                    me.fireEvent('save', me, me.returnData(id));
                } else {
                    me.fireEvent('save', me, id);
                }

                if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
            } else {
                me.fireEvent('saveerror', me);
                JShell.Msg.error(data.msg);
            }
        });
    },
	/**根据主键ID加载数据*/
	load:function(id){
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
			
		if(!id) return;
		
		me.PK = id;//面板主键
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.showMask(me.loadingText);//显示遮罩层
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&" ) + "id=" + id;
		url += '&fields=' + me.getStoreFields().join(',');
		
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(data.value){
					data.value = JShell.Server.Mapping(data.value);
					console.log(data.value);
					//查询区域名称
					if(data.value.CLIENTELE_AreaID && data.value.CLIENTELE_AreaID != "" && data.value.CLIENTELE_AreaID != "0"){
						var areaurl=JShell.System.Path.ROOT+me.selectAreaUrl+"&fields=ClientEleArea_AreaCName,ClientEleArea_Id&id="+data.value.CLIENTELE_AreaID;
						JShell.Server.get(areaurl,function(dataa){
							if(dataa.success){
								dataa.value = JShell.Server.Mapping(dataa.value);
								if(dataa.value && dataa.value.ClientEleArea_AreaCName){
									data.value["CLIENTELE_AreaName"] = dataa.value.ClientEleArea_AreaCName
								}
							}							
						},false)
					}
					me.lastData = me.changeResult(data.value);					
	                me.getForm().setValues(data.value);
	            }
			}else{
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load',me,data);
		});
	}
});