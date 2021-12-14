/**
 * 仪器清单
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.EquipInfoListForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '仪器清单',
	/**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContractFollow',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowStatus',
	PK:null,
	width: 1366,
	height: 400,
	formtype: "add",
	/**显示成功信息*/
	showSuccessInfo: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**开启加载数据遮罩层*/
	hasLoadMask: false,
	autoScroll: false,
	layout: 'fit',
	bodyPadding: '0px 0px 1px 0px',
	/**是否启用保存按钮*/
	hasSave:true,
    /**是否重置按钮*/
	hasReset:true,
	 /**是否暂存按钮*/
	hasTemSave:true,
	/**是否启用取消按钮*/
	hasCancel:true,
	initComponent: function() {
    	var me = this;
		me.buttonToolbarItems = ['->'];
		if(me.hasTemSave) {
			me.buttonToolbarItems.push({
				text: '暂存',
				iconCls: 'button-save',
				tooltip: '暂存',
				handler: function() {
					me.onSave(false);
				}
			});
		}
		if(me.hasSave) {
			me.buttonToolbarItems.push({
				text: '提交',
				iconCls: 'button-save',
				tooltip: '提交',
				handler: function() {
					me.onSave(true);
				}
			});
		}
		if(me.hasReset) {
			me.buttonToolbarItems.push('reset');
		}
		me.callParent(arguments);
	},
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.formtype=='add'){
			var LinkEquipInfoListHTML=me.getComponent('PContractFollow_LinkEquipInfoListHTML');
			var value='<TABLE borderColor=#000000 cellSpacing=2 cellPadding=3 width="100%"bgColor=#ffffff border=1 heihgt=""><TBODY><TR><TD><FONT face=黑体size=3>仪器名称</FONT></TD><TD><FONT face=黑体size=3>仪器型号</FONT></TD><TD><FONT face=黑体size=3>是否双向</FONT></TD><TD><FONT face=黑体size=3>是否合同内</FONT></TD><TD><FONT face=黑体size=3>进场核实</FONT></TD><TD><FONT face=黑体size=3>验收时间</FONT></TD><TD><FONT face=黑体size=3>开始时间</FONT></TD><TD><FONT face=黑体size=3>完成时间</FONT></TD><TD><P><FONT face=黑体size=3>已有/<BR>重新配置/<BR>定制</FONT></P></TD><TD><FONT face=黑体size=3>站点</FONT>&nbsp;</TD></TR><TR><TD>&nbsp;强生</TD><TD>&nbsp;250</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR></TBODY></TABLE>';
			LinkEquipInfoListHTML.setValue(value);
		}
	},

	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight * 0.725;
		height = (height > 385 ? height : 385);

		items = [{
			name: 'PContractFollow_LinkEquipInfoListHTML',
			itemId: 'PContractFollow_LinkEquipInfoListHTML',
			margin: '0px 0px 0px 0px',
			xtype: 'ueditor',
			width: '100%',
			height: height,
			autoScroll: true,
			border: false
		},{
        	fieldLabel:'状态',name:'PContractFollow_ContractStatus',itemId:'PContractFollow_ContractStatus',hidden:true
        },{
			fieldLabel:'主键ID',name:'PContractFollow_Id',hidden:true
		}];
		return items;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
		/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractFollowStatus',function(){
			if(!JShell.System.ClassDict.PContractFollowStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}
			if(isSubmit){//提交
    			var info = JShell.System.ClassDict.getClassInfoByName('PContractFollowStatus','申请');
				me.getForm().setValues({
					PContractFollow_ContractStatus:info.Id
				});
			}else{//暂存
				var info = JShell.System.ClassDict.getClassInfoByName('PContractFollowStatus','暂存');
				me.getForm().setValues({
					PContractFollow_ContractStatus:info.Id
				});
			}
			
			//新增时需要校验合同编号是否不重复
			if(values.PContractFollow_Id){
				me.onSaveClick();
			}
    	});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
			
		var entity = {
			ContractStatus:values.PContractFollow_ContractStatus,//状态
            LinkEquipInfoListHTML:values.PContractFollow_LinkEquipInfoListHTML
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = ['Id','ContractStatus','LinkEquipInfoListHTML'];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PContractFollow_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var value='<TABLE borderColor=#000000 cellSpacing=2 cellPadding=3 width="100%"bgColor=#ffffff border=1 heihgt=""><TBODY><TR><TD><FONT face=黑体size=3>仪器名称</FONT></TD><TD><FONT face=黑体size=3>仪器型号</FONT></TD><TD><FONT face=黑体size=3>是否双向</FONT></TD><TD><FONT face=黑体size=3>是否合同内</FONT></TD><TD><FONT face=黑体size=3>进场核实</FONT></TD><TD><FONT face=黑体size=3>验收时间</FONT></TD><TD><FONT face=黑体size=3>开始时间</FONT></TD><TD><FONT face=黑体size=3>完成时间</FONT></TD><TD><P><FONT face=黑体size=3>已有/<BR>重新配置/<BR>定制</FONT></P></TD><TD><FONT face=黑体size=3>站点</FONT>&nbsp;</TD></TR><TR><TD>&nbsp;强生</TD><TD>&nbsp;250</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD></TR></TBODY></TABLE>';
		if(!data.PContractFollow_LinkEquipInfoListHTML){
			data.PContractFollow_LinkEquipInfoListHTML = value;
		}
		return data;
	}
});