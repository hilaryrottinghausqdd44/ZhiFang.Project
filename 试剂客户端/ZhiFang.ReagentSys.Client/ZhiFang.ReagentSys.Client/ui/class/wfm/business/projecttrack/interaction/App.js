/**
 * 项目跟踪记录
 * @author liangyl
 * @version 2017-08-09
 */
Ext.define('Shell.class.wfm.business.projecttrack.interaction.App',{
    extend:'Shell.class.sysbase.interaction.App',
	/**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractFollowInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContractFollowInteraction',
    /**附件对象名*/
	objectName:'PContractFollowInteraction',
	/**附件关联对象名*/
	fObejctName:'PContractFollow',
	/**附件关联对象主键*/
	fObjectValue:'',
	
	/**项目跟踪ID*/
	PK:'',
	/**标题*/
    title:'互动信息',
    width:800,
    height:600,
    
	fObjectIsID:false,
	/**展示方式，支持 e(东) w(西) s(南) n(北) */
	FormPosition:'s',
	/**默认条件*/
	defaultWhere:'',
	/**提交后刷新列表*/
	IsSaveToLoad:true,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.selectUrl && me.addUrl && me.objectName && me.fObejctName && me.fObjectValue){
			me.Form.on({
				save:function(p,id){
					p.getComponent('Content').setValue('');
					if(me.IsSaveToLoad){
						me.Grid.onSearch();
					}
					me.fireEvent('save', me);
				}
			});
		}
	},
    
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.fObjectValue = me.PK;
		if(me.selectUrl && me.addUrl && me.objectName && me.fObejctName && me.fObjectValue){
			me.items = me.createItems();
		}else{
			me.html =
				'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">'+
					'请传递selectUrl、addUrl、objectName、fObejctName、fObjectValue参数！'+
				'</div>';
		}
		
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.wfm.business.projecttrack.interaction.basic.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			defaultLoad: true,
			selectUrl:me.selectUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectValue:me.fObjectValue,
			fObjectIsID:me.fObjectIsID,
			defaultWhere:me.defaultWhere
		});
		
		var FormConfig = {
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			addUrl:me.addUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectValue:me.fObjectValue,
			fObjectIsID:me.fObjectIsID
		};
		
		if(me.FormPosition == 'w'){
			FormConfig.region = 'west';
			FormConfig.width = 250;
		}else if(me.FormPosition == 'e'){
			FormConfig.region = 'east';
			FormConfig.width = 250;
		}else if(me.FormPosition == 's'){
			FormConfig.region = 'south';
			FormConfig.height = 150;
		}else if(me.FormPosition == 'n'){
			FormConfig.region = 'north';
			FormConfig.height = 150;
		}if(me.FormPosition == 'w'){
			FormConfig.region = 'west';
			FormConfig.width = 250;
		}
		
		
		me.Form = Ext.create('Shell.class.sysbase.interaction.model1.Form', FormConfig);
		
		return [me.Grid,me.Form];
	}
});
	