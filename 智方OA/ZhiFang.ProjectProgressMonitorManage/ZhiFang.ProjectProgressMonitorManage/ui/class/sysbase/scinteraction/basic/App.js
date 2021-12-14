/**
 * 互动交流应用
 * @author longfc
 * @version 2017-03-21
 */
Ext.define('Shell.class.sysbase.scinteraction.basic.App', {
	extend: 'Shell.ux.panel.AppPanel',

	/**标题*/
	title: '互动交流信息',
	width: 680,
	height: 620,

	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCInteractionByHQL?isPlanish=true',
	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息*/
	selectAllUrl: '/SystemCommonService.svc/SC_UDTO_SearchAllSCInteractionByBobjectID?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SystemCommonService.svc/SC_UDTO_AddSCInteractionExtend',
	/**交流对象名*/
	objectName: '',
	/**交流关联对象名*/
	fObejctName: '',
	/**交流关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,

	/**默认条件*/
	defaultWhere: '',

	/**提交后刷新列表*/
	IsSaveToLoad: true,
	border: false,
	bodyPadding: '2px 2px 2px 2px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},

	initComponent: function() {
		var me = this;
		if(me.objectName && me.fObejctName && me.fObjectValue) {
			me.items = me.createItems();
		} else {
			me.html =
				'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">' +
				'请传递objectName、fObejctName、fObjectValue参数！' +
				'</div>';
		}
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.TopicList = Ext.create('Shell.class.sysbase.scinteraction.basic.TopicList', {
			region: 'west',
			header: false,
			split: true,
			//border:false,
			collapsible: true,
			width: 380,
			itemId: 'TopicList',
			defaultLoad: true,
			selectUrl: me.selectUrl,
			objectName: me.objectName,
			fObejctName: me.fObejctName,
			fObjectValue: me.fObjectValue,
			fObjectIsID: me.fObjectIsID,
			defaultWhere: me.defaultWhere
		});
		me.InteractionApp = Ext.create('Shell.class.sysbase.scinteraction.basic.InteractionApp', {
			region: 'center',
			header: false,
			//border: false,
			selectUrl: me.selectUrl,
			selectAllUrl: me.selectAllUrl,
			addUrl: me.addUrl,
			itemId: 'InteractionApp',
			objectName: me.objectName,
			fObejctName: me.fObejctName,
			fObjectValue: me.fObjectValue,
			fObjectIsID: me.fObjectIsID
		});
		return [me.TopicList, me.InteractionApp];
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		var TopicList = me.getComponent('TopicList');
		var buttonsToolbar = TopicList.getComponent('buttonsToolbar');
		var showAllInteraction = buttonsToolbar.getComponent('showAllInteraction');
		if(me.objectName && me.fObejctName && me.fObjectValue) {
			//话题列表
			me.TopicList.on({
				select: function(rowModel, record, eOpts) {
					showAllInteraction.setValue(false);
					me.InteractionApp.setBtnInteraction(false);
					var fObjectValue = record.get(me.objectName + '_Id');
					me.InteractionApp.InteractionList.autoSelect = false;
					me.linkageLoadData(fObjectValue, false);
				},
				nodata: function() {
					me.linkageClearData();
				}
			});
			//话题列表的全部交流复选框
			showAllInteraction.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue == true) {
						me.InteractionApp.setBtnInteraction(true);
						me.TopicList.autoSelect = false;
						me.InteractionApp.InteractionList.autoSelect = true;
						me.linkageLoadData(me.fObjectValue, true);
					} else {
						me.TopicList.autoSelect = true;
						me.InteractionApp.InteractionList.autoSelect = true;
					}
				}
			});
			//交流应用
			me.InteractionApp.on({
				//新增话题后
				onAddTopic: function(app, form, id) {
					me.TopicList.autoSelect = true;
					me.InteractionApp.InteractionList.autoSelect = false;
					me.TopicList.onSearch();
				},
				//新增交流后,回复个数和时间更新
				onAddInteraction: function(app, form, bobjectID) {
					var checked = showAllInteraction.getValue();
					me.InteractionApp.InteractionList.onSearch();
					setTimeout(function() {
						var record = me.TopicList.store.findRecord(me.TopicList.PKField, bobjectID);
						if(record) {
							var replyCountKey = me.objectName + '_ReplyCount';
							var replyCount = record.get(replyCountKey);
							if(replyCount) replyCount = parseInt(replyCount);
							if(checked != true) {
								replyCount = me.InteractionApp.InteractionList.replyCount;
							} else {
								replyCount = 0;
								me.InteractionApp.InteractionList.store.each(function(record) {
									if(record.get(me.objectName + '_BobjectID') == bobjectID) {
										replyCount = replyCount + 1;
									}
								});
							}
							record.set(replyCountKey, replyCount);

							var rastReplyDateTimeKey = me.objectName + '_LastReplyDateTime';
							var Sysdate = JcallShell.System.Date.getDate();
							if(Sysdate == null) Sysdate = new Date();
							//HH:mm:ss
							var rastReplyDateTimeValue = JShell.Date.toString(Sysdate, false) || '';
							record.set(rastReplyDateTimeKey, rastReplyDateTimeValue);
						}
					}, 1000);
				}
			});
			//交流列表
			me.InteractionApp.InteractionList.on({
				select: function(rowModel, record, eOpts) {
					showAllInteraction.setValue(false);

					var fObjectValue = record.get(me.objectName + '_BobjectID');
					me.InteractionApp.fObjectValue = fObjectValue;
					me.InteractionApp.Form.fObjectValue = fObjectValue;
					me.InteractionApp.InteractionList.loadAllData = false;
					me.InteractionApp.setBtnInteraction(false);
				}
			});
			//交流表单的话题按钮单击事件
			me.InteractionApp.Form.on({
				onTopicClick: function(form) {
					me.InteractionApp.Form.fObjectValue = me.fObjectValue;
					me.InteractionApp.fObjectValue = me.fObjectValue;
				}
			});
		}
	},
	/**话题列表选择行后联动交流应用*/
	linkageClearData: function() {
		var me = this;
		setTimeout(function() {
			me.InteractionApp.fObjectValue = me.fObjectValue;
			me.InteractionApp.setBtnInteraction(true);
			me.InteractionApp.clearData();
		}, 300);
	},
	/**话题列表选择行后联动交流应用*/
	linkageLoadData: function(fObjectValue, isLoadAllData) {
		var me = this;
		setTimeout(function() {
			me.InteractionApp.fObjectValue = fObjectValue;
			me.InteractionApp.loadData(isLoadAllData);
		}, 500);
	}
});