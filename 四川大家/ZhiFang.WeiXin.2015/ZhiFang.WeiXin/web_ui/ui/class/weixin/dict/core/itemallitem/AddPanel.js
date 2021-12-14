/**
 * 项目表单和组套项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.AddPanel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目表单和组套项目',
    border:false,
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddItemAllItemVO',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateItemAllItemByFieldVO', 
    ItemNo:null,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.showMask();
		me.Form.on({
			save:function(p,id){
				me.fireEvent('save', p,id);
			},
			onSaveClick: function(){
		    	me.onSave();
		    },
			load:function(form,data){
			}
		});
	},
    
	initComponent:function(){
		var me = this;
	    me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Form = Ext.create('Shell.class.weixin.dict.core.itemallitem.Form', {
			region: 'north',
			header: false,height:335,
			itemId: 'Form',split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.weixin.dict.core.itemallitem.GroupItemGrid', {
			region: 'center',
			header: false,
			ItemNo:me.ItemNo,
			itemId: 'Grid'
			
		});
		return [me.Form,me.Grid];
	},
	/**重新加载*/
	loadData:function(id){
		var me=this;
		me.clearData();
	    if(id){
	    	me.Form.isShow(id);
	    	me.Grid.ItemNo=id;
	        me.Grid.onSearch();
	    }
	    var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var Edit=buttonsToolbar.getComponent('Edit');
		Edit.setDisabled(true);
		var btnDel=buttonsToolbar.getComponent('btnDel');
		btnDel.setDisabled(true);
	    me.hideMask();
	},
	
	clearData:function(){
		var me =this;
        me.showMask();		
		me.Form.clearData();
        me.Grid.clearData();
	},
	  /**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	onSave:function(){
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var values = me.Form.getForm().getValues();		
		 //新增校验项目编号是否存在相同的
		if(me.Form.formtype=='add' && values.TestItem_Id){
	        var isExec=true;
	        //校验项目编号
	         me.Form.getisValidItemNo(function(data){
	        	if(data && data.value){
	        		var len = data.value.list.length;
	        		if(len>0){
	        			var ItemNo=data.value.list[0].TestItem_Id;
	        			isExec=false;
	        			JShell.Msg.error('项目编号:【'+ItemNo+'】已存在！');
	        		}
	        	}
	        });
	        if(!isExec) return;
        }
		me.showMask();
		//套餐项目信息
		var entity = me.Form.getAddParams();
		
		var	values = me.Form.getForm().getValues();
		//获取明细信息
		var entityInfo = me.Grid.getEntity(values.TestItem_Id);
        var fields = [ 'IsDoctorItem','CName',
			'StandardCode', 'IschargeItem', 'EName', 'ShortCode',
			'IsCombiItem', 'ShortName', 'IsProfile','Price','Unit',
			'IsCalc',
			'Prec','Cuegrade',
			'DiagMethod','Secretgrade',
			'BonusPercent','Color','Visible',
			'SuperGroupNo','DispOrder','OrderNo',
			'ItemDesc','Id','FWorkLoad'
		];
	    fields = fields.join(',');
		var obj={
            Unit:entity.Unit,
            CName:entity.CName,
            Color:entity.Color,
            Cuegrade:entity.Cuegrade,
            DiagMethod:entity.DiagMethod,
            EName:entity.EName,
            IsCalc:entity.IsCalc,
            IsCombiItem:entity.IsCombiItem,
            IsDoctorItem:entity.IsDoctorItem,
            IsProfile:entity.IsProfile,
            IschargeItem:entity.IschargeItem,
            ItemDesc:entity.ItemDesc,
			SubTestItemVO:entityInfo,
			ShortCode:entity.ShortCode,
			StandardCode:entity.StandardCode,
			ShortName:entity.ShortName,
			OrderNo:entity.OrderNo,
			Visible:entity.Visible
		};
		
		if(entity.SuperGroupNo){
			obj.SuperGroupNo=entity.SuperGroupNo;
		}
		if(entity.DispOrder){
			obj.DispOrder=entity.DispOrder;
		}
		if(entity.BonusPercent){
			obj.BonusPercent=entity.BonusPercent;
		}
		if(entity.FWorkLoad){
			obj.FWorkLoad=entity.FWorkLoad;
		}
		if(entity.Price){
			obj.Price=entity.Price;
		}
		if(values.TestItem_Id){
			obj.Id=values.TestItem_Id;
		}
		if(entity.Prec){
			obj.Prec= entity.Prec
		}
		if(entity.Secretgrade){
			obj.Secretgrade= entity.Secretgrade
		}
		if(me.Form.formtype=='edit'){
			obj.IsCombiItem=1;
			obj.IsProfile=1;
			obj.IsDoctorItem=1;
			var url = JShell.System.Path.getUrl(me.editUrl);
			var params = Ext.JSON.encode({entity:obj,fields:fields});		
		}else{
			var url = JShell.System.Path.getUrl(me.addUrl);
			var params = Ext.JSON.encode({entity:obj});		
		}

		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.clearData();
				me.fireEvent('save',me);
			    JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
		
	},
	onAddClick:function(id){
		var me =this;
		me.hideMask();
		
		me.Form.isAdd(id);
		me.Grid.clearData();
		me.Grid.formtype='add';
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var Edit=buttonsToolbar.getComponent('Edit');
		Edit.enable();
		var btnDel=buttonsToolbar.getComponent('btnDel');
		btnDel.enable();
		
	},
	onEditClick:function(id){
		var me =this;
		me.Form.isEdit(id);
		me.Grid.formtype='edit';
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var Edit=buttonsToolbar.getComponent('Edit');
		Edit.enable();
		var btnDel=buttonsToolbar.getComponent('btnDel');
		btnDel.enable();
	}
});