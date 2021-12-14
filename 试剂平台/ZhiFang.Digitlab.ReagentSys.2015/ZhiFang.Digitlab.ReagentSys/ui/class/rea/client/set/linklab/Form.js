/**
 * 实验室数据库链接配置
 * @author liangyl
 * @version 2017-10-13
 */
Ext.define('Shell.class.rea.client.set.linklab.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '实验室数据库链接配置',
	
	width:240,
    height:210,
    /**测试数据库ADO连接字符串*/
	LinkUrl:'/ReaADOService.svc/RADOS_UDTO_CheckDataBaseLinkByConnectStr',

	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**内容周围距离*/
	bodyPadding:'10px 0px 0px 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:65,
        labelAlign:'right'
    },
    /**列表store,用于判断平台数据是否已存在*/
	FieldNameStore:null,
	/**选择行数据*/
	recvalues:{},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
	
		//机构编号
		items.push({
			fieldLabel: '机构编号',name: 'OrgNo',itemId: 'OrgNo',emptyText:'必填项',hidden:true,allowBlank:false
		});
		//机构名称
		items.push({
			fieldLabel:'机构',name:'OrgName',itemId:'OrgName',
			emptyText:'必填项',allowBlank:false,
			locked:false,readOnly:false,
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				title:'实验室机构选择',
				width:330,
				height:380,
				defaultWhere:"cenorg.CenOrgType.ShortCode='3'"		
			}
		});
		//数据库驱动
		items.push({
			fieldLabel:'数据库驱动',name:'DriverName',itemId:'DriverName',
//			emptyText:'必填项',allowBlank:false,
			hidden:true
		});
		//服务器名称
		items.push({
			fieldLabel:'服务器名称',name:'ServerName',itemId:'ServerName',
			emptyText:'必填项',allowBlank:false
		});
		//数据库名称
		items.push({
			fieldLabel:'数据库名称',emptyText:'必填项',allowBlank:false,
			name:'DatabaseName',itemId:'DatabaseName'
		});
		//用户名
		items.push({
			fieldLabel:'用户名',emptyText:'必填项',allowBlank:false,
			name:'UserName',itemId:'UserName'
		});
		//密码
		items.push({
			fieldLabel:'密码',emptyText:'必填项',allowBlank:false,
			name:'Password',itemId:'Password',inputType: 'password' 
		});
		//是否使用
		items.push({
			fieldLabel:'是否使用',name:' Visible',hidden:true,itemId:'Visible'
		});
		return items;
	},
	/**创建功能按钮栏
	 * 重写添加测试链接按钮
	 * */
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0){
			items.push({
				text:'测试链接',tooltip:'测试链接',iconCls:'button-help',
				handler:function(btn){
					me.LinkSaveInfo(btn);
				}
			});
			if(me.hasSave) items.push('->','save');
			if(me.hasSaveas) items.push('saveas');
			if(me.hasReset) items.push('reset');
			if(me.hasCancel) items.push('cancel');
//			if(items.length > 0) items.unshift('->');
		}
		
		if(items.length == 0) return null;
		
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//机构
		var OrgName = me.getComponent('OrgName'),
			OrgNo = me.getComponent('OrgNo');
		OrgName.on({
			check: function(p, record) {
				OrgName.setValue(record ? record.get('CenOrg_CName') : '');
				OrgNo.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
			values = me.getForm().getValues();
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
		if(me.FieldNameStore && me.formtype=='add'){
			var record = me.FieldNameStore.findRecord('OrgNo', values.OrgNo);
	        if(record){
	        	JShell.Msg.error('该实验室数据库链接已配置');
	        	return;
	        }
		}
		var entity = {
			OrgNo:values.OrgNo,
			OrgName:values.OrgName,
//			DriverName:values.DriverName,
			ServerName:values.ServerName,
			DatabaseName:values.DatabaseName,
			UserName:values.UserName,
			Password:values.Password
//			Visible:values.Visible
		}
		me.fireEvent('save',me,entity);
	},
	/**更改标题*/
	changeTitle:function(){
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			this.getForm().reset();
		}
		if(me.formtype=='edit'){
			me.isEdit();
		}
	},
	/**初始化信息数据*/
	initInfo:function(){
		var me = this,
			type = me.formtype,
			id = me.PK;
		
		if(type == 'add'){
            me.isAdd();
        }else if(type == 'edit'){
            	me.isEdit(id);
        }else if(type == 'show'){
			if(me.PK){
				me.isShow(id);
			}else{
            	me.setReadOnly(true);
            }
        }
	},
	isEdit:function(id){
		var me = this;
		
		me.getComponent('buttonsToolbar').show();
		me.setReadOnly(false);
		
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		me.editRed();
	},
	/**@overwrite 还原选择行的值*/
	editRed:function(){
		var me=this;
		//还原选择行的值
		var OrgNo=me.getComponent('OrgNo');
		var OrgName=me.getComponent('OrgName');
		var ServerName=me.getComponent('ServerName');
		var DatabaseName=me.getComponent('DatabaseName');
		var UserName=me.getComponent('UserName');
		var Password=me.getComponent('Password');
		OrgName.setReadOnly(true);
		OrgNo.setValue(me.recvalues.OrgNo);
		OrgName.setValue(me.recvalues.OrgName);
		ServerName.setValue(me.recvalues.ServerName);
		DatabaseName.setValue(me.recvalues.DatabaseName);
		UserName.setValue(me.recvalues.UserName);
		Password.setValue(me.recvalues.Password);
	},
	/**@overwrite 测试连接*/
	LinkSaveInfo: function(btn) {
		var me=this;
	    var url = JShell.System.Path.getRootUrl(me.LinkUrl);
	    var orgNo=JShell.REA.System.CENORG_ID;
	    var orgName=JShell.REA.System.CENORG_NAME;
	    var values = me.getForm().getValues();
	    if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
	    me.showMask(me.loadingText);//显示遮罩层
	    var userid=values.UserName;
	    var database=values.DatabaseName;
	    var Server=values.ServerName;
	    var Password=values.Password;
	    var dbConnectStr="Server="+Server+";database="+database+";uid="+userid+";pwd="+Password+";";
        url+='?orgNo='+orgNo+'&orgName='+orgName+'&dbConnectStr='+dbConnectStr;
	    
	    JShell.Server.get(url,function(data) {
	    	me.hideMask();//隐藏遮罩层
			if (data.success) {
				JShell.Msg.alert('数据库连接成功!');
			} else {
				JShell.Msg.error('数据库连接失败!');
			}
		},false);
	}
});