/**
 * 备注和附件
 * @author liangyl
 * @version 2016-08-25
 */
Ext.define('Shell.class.qms.equip.register.MemoTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '备注和附件',
	layout: 'fit',
	autoScroll: false,
	PK: '',
	formtype: '',
	/**附件是否已加载*/
	IsAttachmentLoad: false,
	TempletID: null,
	/**月保养*/
	TempletType: '',
	/**月保养编码*/
	TempletTypeCode: '',
	/**开始时间*/
	startDate: null,
	/**结束时间*/
	endDate: null,
	//审核类型，默认按月
	CheckType:'0',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ontabchange();
		me.on({
			beforesave: function(p) {
				me.showMask(me.saveText);
			},
			aftersave: function(p, id) {
				me.hideMask();
				me.PK = id;
				me.onSaveAttachment(id);
			}
		});
		me.Attachment.on({
			save: function(win, data) {
				if(data.success) {
					me.fireEvent('save', me, me.PK);
				} else {
					JShell.Msg.error(data.msg);
				}
			},
			uploadClick:function(com){
				me.fireEvent('uploadClick',com);
			}
		});

	},

	initComponent: function() {
		var me = this;
		me.addEvents('uploadClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.qms.equip.register.MemoForm', {
			title: '备注',
			header: false,
			layout: "fit",
			itemId: 'Form'
		});

		me.Attachment = Ext.create('Shell.class.qms.equip.templet.emaintenancedata.AttachmentGrid', {
			region: 'center',
			header: false,
			title: '附件',
			itemId: 'Attachment',
			border: false,
			defaultLoad: false,
			PK: me.PK,
			/**月保养*/
			TempletType: me.TempletType,
			/**月保养编码*/
			TempletTypeCode: me.TempletTypeCode,
			SaveCategory: "EAttachment",
			BusinessModuleCode: "EAttachment",
			/**开始时间*/
			startDate: me.startDate,
			/**结束时间*/
			endDate: me.endDate,
			TempletID: me.PK,
			formtype: me.formtype
		});
		return [me.Form, me.Attachment];
	},

	onSaveAttachment: function(id) {
		var me = this;
		me.Attachment.setValues({
			fkObjectId: id
		});
		me.Attachment.save();
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
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var url = me.Form.formtype == 'add' ? me.Form.addUrl : me.Form.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params = me.Form.formtype == 'add' ? me.Form.getAddParams() : me.Form.getEditParams();
		if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		var values = me.Form.getForm().getValues();
		me.fireEvent('beforesave', me);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				me.fireEvent('aftersave', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			me.Form.getForm().reset();
		} else {
			me.Form.getForm().setValues(me.Form.lastData);
		}
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {

				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'Form':
						break;
					case 'Attachment':
						me.loadDataAttachment();
						break;
					default:
						break
				}
			}
		});
	},
	/**加载附件*/
	loadDataAttachment: function() {
		var me = this;
		if(me.IsAttachmentLoad == false && me.PK) {
			me.Attachment.PK = me.PK;
			me.Attachment.TempletType=me.TempletType;
			me.Attachment.TempletTypeCode=me.TempletTypeCode;
			
			me.Attachment.endDate=JShell.Date.toString(me.endDate,true);
		    me.Attachment.startDate=JShell.Date.toString(me.startDate,true);
			
			me.Attachment.onSearch();
			me.IsAttachmentLoad = true;
		}
	}
});