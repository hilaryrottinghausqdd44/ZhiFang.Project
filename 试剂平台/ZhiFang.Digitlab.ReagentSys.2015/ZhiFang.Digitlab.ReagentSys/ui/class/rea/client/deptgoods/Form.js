/**
 * 部门产品维护
 * @author liangyl
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.deptgoods.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '部门货品信息',
	
	width:250,
    height:230,
	
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsById?isPlanish=true',
    /**获取数据服务路径，验证关系是否已存在*/
    selectUrl2:'/ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsByHQL?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaDeptGoods',
    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoodsByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 5px 0px 0px',
	/**布局方式*/
	layout:'anchor',
	PK:null,
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:70,
        labelAlign:'right'
    },
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
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
			
		//主键ID
		items.push({fieldLabel:'主键ID',name:'ReaDeptGoods_Id',hidden:true,type:'key'});		
		//货品
		items.push({
			fieldLabel:'货品',emptyText:'必填项',allowBlank:false,
			name:'ReaDeptGoods_GoodsCName',itemId:'ReaDeptGoods_GoodsCName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig:{
				title:'货品选择',
				/**是否单选*/
	            checkOne:true
	
			}
		},{
			fieldLabel:'货品主键ID',hidden:true,name:'ReaDeptGoods_ReaGoods_Id',itemId:'ReaDeptGoods_ReaGoods_Id'
		});
		 //部门
		items.push({
			fieldLabel:'部门',emptyText:'必填项',allowBlank:false,
			name:'ReaDeptGoods_DeptName',itemId:'ReaDeptGoods_DeptName',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.sysbase.org.CheckTree',{
					resizable:false,
					listeners:{
						accept:function(p,record){
							var	ParentID = me.getComponent('ReaDeptGoods_HRDept_Id'),
			                    ParentName = me.getComponent('ReaDeptGoods_DeptName');
							ParentID.setValue(record.get('tid'));
		                    ParentName.setValue(record.get('text') || '');
							p.close();}
					}
				}).show();
			}
		},{
			fieldLabel:'部门主键ID',hidden:true,name:'ReaDeptGoods_HRDept_Id',itemId:'ReaDeptGoods_HRDept_Id'
		});

		//显示次序
		items.push({
			fieldLabel:'显示次序',name:'ReaDeptGoods_DispOrder',emptyText:'必填项',allowBlank:false,xtype:'numberfield',value:0
		});
		//是否使用
		items.push({
			fieldLabel:'启用',name:'ReaDeptGoods_Visible',xtype:'uxBoolComboBox',value:true,hasStyle:true
		});

		//备注
		items.push({
			height:40,
			xtype:'textarea',fieldLabel:'备注',name:'ReaDeptGoods_Memo'
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Memo:values.ReaDeptGoods_Memo,
			DispOrder:values.ReaDeptGoods_DispOrder,
			Visible:values.ReaDeptGoods_Visible ? 1 : 0
		};
		if(values.ReaDeptGoods_ReaGoods_Id){
			entity.ReaGoods = {
				Id:values.ReaDeptGoods_ReaGoods_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.GoodsCName=values.ReaDeptGoods_GoodsCName;
		}
		if(values.ReaDeptGoods_HRDept_Id){
			entity.HRDept = {
				Id:values.ReaDeptGoods_HRDept_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.DeptName=values.ReaDeptGoods_DeptName;
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
			
		var fields = [
			'Id','DispOrder','Visible','ReaGoods_Id','HRDept_Id','DeptName','GoodsCName'
		];
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.ReaDeptGoods_Id;
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var GoodsId=me.getComponent('ReaDeptGoods_ReaGoods_Id');
		var GoodsCName=me.getComponent('ReaDeptGoods_GoodsCName');
        GoodsCName.on({
        	check: function(p, record) {
				GoodsCName.setValue(record ? record.get('ReaGoods_CName') : '');
				GoodsId.setValue(record ? record.get('ReaGoods_Id') : '');
				p.close();
			}
        });
       
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.ReaDeptGoods_Visible = data.ReaDeptGoods_Visible == '1' ? true : false;
		
		return data;
	},
	/**更改标题*/
	changeTitle:function(){
	},
	/**根据IDS获取关系数据，用于验证勾选的货品是否已经存在于关系中*/
	getLinkByIds:function(callback){
		var me = this,values = me.getForm().getValues(),
			url = JShell.System.Path.ROOT + me.selectUrl2.split('?')[0] + 
				'?fields=ReaDeptGoods_ReaGoods_Id' +
				'&where=readeptgoods.ReaGoods.Id =' + values.ReaDeptGoods_ReaGoods_Id + '  and readeptgoods.HRDept.Id='+ values.ReaDeptGoods_HRDept_Id +' and readeptgoods.Id!='+me.PK ;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		// 验证已存在的关系不允许重复添加
		var isExec=true;
		me.getLinkByIds(function(data){
			if(data && data.length>0){
				isExec=false;
				JShell.Msg.error('关系已存在，不能重复添加');
				return;
			}
		});
		if(!isExec) return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});