/**
 * @description 预警提示信息
 * @author liangyl
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.warningalertinfo.App', {
	extend: 'Ext.tab.Panel',

	title: '预警提示信息',
	//	header: false,
	border: false,
	bodyPadding: 1,
	//activeTab: 0,

	selectUrl: '/ReaManageService.svc/RS_UDTO_GetReaGoodsWarningAlertInfo',

	/**库存报警*/
	StoreAlarm: {},
	/**效期报警*/
	ExpirationAlarm: {},
	/**注册证报警*/
	RegistAlarm: {},
	/**数据是否已加载*/
	isStoreLowerPanelLoad: false,
	isStoreUpperPanelLoad: false,
	isValidityExpiredPanelLoad: false,
	isValidityWillExpirePanelLoad: false,
	isRegisterExpiredPanelLoad: false,
	isRegisterWillExpirePanelLoad: false,
	isOpenBottleOperGridLoad: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if (me.items.length == 0) return;
		if (me.items.length > 0) me.activeTab = 0;
		//当前激活的页签项
		var comtab = me.getActiveTab(me.items.items[0]);
		comtab.loadData();
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch (newCard.itemId) {
					case 'StoreLowerPanel':
						if (!me.isStoreLowerPanelLoad) {
							me.StoreLowerPanel.loadData();
							me.isStoreLowerPanelLoad = true;
						}
						break;
					case 'StoreUpperPanel':
						if (!me.isStoreUpperPanelLoad) {
							me.StoreUpperPanel.loadData();
							me.isStoreUpperPanelLoad = true;
						}
						break;
					case 'ValidityExpiredPanel':
						if (!me.isValidityExpiredPanelLoad) {
							me.ValidityExpiredPanel.loadData();
							me.isValidityExpiredPanelLoad = true;
						}
						break;
					case 'ValidityWillExpirePanel':
						if (!me.isValidityWillExpirePanelLoad) {
							me.ValidityWillExpirePanel.loadData();
							me.isValidityWillExpirePanelLoad = true;
						}
						break;
					case 'RegisterExpiredPanel':
						if (!me.isRegisterExpiredPanelLoad) {
							me.RegisterExpiredPanel.loadData();
							me.isRegisterExpiredPanelLoad = true;
						}
						break;
					case 'RegisterWillExpirePanel':
						if (!me.isRegisterWillExpirePanelLoad) {
							me.RegisterWillExpirePanel.loadData();
							me.isRegisterWillExpirePanelLoad = true;
						}
						break;
					case 'OpenBottleOperGrid':
						if (!me.isOpenBottleOperGridLoad) {
							me.OpenBottleOperGrid.onSearch();
							me.isOpenBottleOperGridLoad = true;
						}
						break;
					default:

						break
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		//判读是否为空对象
		var StoreAlarmNum = Object.keys(me.StoreAlarm).length;
		var ExpirationAlarmNum = Object.keys(me.ExpirationAlarm).length;
		var RegistAlarmNum = Object.keys(me.RegistAlarm).length;
		//不存在数据时调用服务
		if (StoreAlarmNum === 0 && ExpirationAlarmNum === 0 && RegistAlarmNum === 0) {
			me.getWarning(function(data) {
				me.StoreAlarm = data.StoreAlarm;
				me.ExpirationAlarm = data.ExpirationAlarm;
				me.RegistAlarm = data.RegistAlarm;
			});
		}
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.StoreLowerPanel = Ext.create('Shell.class.rea.client.qtywarning.storelower.Panel', {
			title: '低库存预警',
			header: true,
			border: false,
			itemId: 'StoreLowerPanel'
		});
		me.StoreUpperPanel = Ext.create('Shell.class.rea.client.qtywarning.storeupper.Panel', {
			title: '高库存预警',
			header: true,
			border: false,
			itemId: 'StoreUpperPanel'
		});
		//效期已过期报警
		var expiredDays = 1;
		if (me.ExpirationAlarm.IsExpirationAlarm && me.ExpirationAlarm.HasExpired) {
			if (me.ExpirationAlarm.ExpiredDays) {
				expiredDays = parseInt(me.ExpirationAlarm.ExpiredDays);
				JcallShell.REA.RunParams.Lists["GoodsValidityWarnDays"].IsLoad = true;
				JcallShell.REA.RunParams.Lists["GoodsValidityWarnDays"].Value = expiredDays2;
			}
		}
		me.ValidityExpiredPanel = Ext.create('Shell.class.rea.client.validitywarning.expired.Panel', {
			title: '效期已过期报警',
			header: true,
			itemId: 'ValidityExpiredPanel',
			expiredDays: expiredDays
		});
		//效期将过期报警
		var willexpireDays = 1;
		if (me.ExpirationAlarm.IsExpirationAlarm && me.ExpirationAlarm.HasWillexpire) {
			if (me.ExpirationAlarm.WillexpireDays) {
				willexpireDays = parseInt(me.ExpirationAlarm.WillexpireDays);
				JcallShell.REA.RunParams.Lists["ExpirationAlarmWillexpireDefaultDays"].IsLoad = true;
				JcallShell.REA.RunParams.Lists["ExpirationAlarmWillexpireDefaultDays"].Value = willexpireDays;
			}
		}
		me.ValidityWillExpirePanel = Ext.create('Shell.class.rea.client.validitywarning.willexpire.Panel', {
			title: '效期将过期报警',
			header: true,
			itemId: 'ValidityWillExpirePanel',
			willexpireDays: willexpireDays
		});
		//注册证已过期报警
		var expiredDays2 = 1;
		if (me.RegistAlarm.IsRegistAlarm && me.RegistAlarm.HasExpired) {
			if (me.RegistAlarm.ExpiredDays) {
				expiredDays2 = parseInt(me.RegistAlarm.ExpiredDays);
				JcallShell.REA.RunParams.Lists["RegistWarnExpiredDefaultDays"].IsLoad = true;
				JcallShell.REA.RunParams.Lists["RegistWarnExpiredDefaultDays"].Value = expiredDays2;
			}
		}
		me.RegisterExpiredPanel = Ext.create('Shell.class.rea.client.registerwarning.expired.Panel', {
			title: '注册证已过期报警',
			header: true,
			itemId: 'RegisterExpiredPanel',
			expiredDays: expiredDays2
		});
		//注册证将过期报警
		var willexpireDays2 = 1;
		if (me.RegistAlarm.IsRegistAlarm && me.RegistAlarm.HasWillexpire) {
			if (me.RegistAlarm.WillexpireDays) {
				willexpireDays2 = parseInt(me.RegistAlarm.WillexpireDays);
				JcallShell.REA.RunParams.Lists["RegistWillexpireWarning"].IsLoad = true;
				JcallShell.REA.RunParams.Lists["RegistWillexpireWarning"].Value = willexpireDays2;
			}
		}
		me.RegisterWillExpirePanel = Ext.create('Shell.class.rea.client.registerwarning.willexpire.Panel', {
			title: '注册证将过期报警',
			header: true,
			itemId: 'RegisterWillExpirePanel',
			willexpireDays: willexpireDays2
		});
		var appInfos = [];
		//低库存报警
		if (me.StoreAlarm.IsStoreAlarm && me.StoreAlarm.HasStoreLower) {
			appInfos.push(me.StoreLowerPanel);
		}
		//高库存预警
		if (me.StoreAlarm.IsStoreAlarm && me.StoreAlarm.HasStoreUpper) {
			appInfos.push(me.StoreUpperPanel);
		}
		//效期已过期报警
		if (me.ExpirationAlarm.IsExpirationAlarm && me.ExpirationAlarm.HasExpired) {
			appInfos.push(me.ValidityExpiredPanel);
		}
		//效期将过期报警
		if (me.ExpirationAlarm.IsExpirationAlarm && me.ExpirationAlarm.HasWillexpire) {
			appInfos.push(me.ValidityWillExpirePanel);
		}
		//注册证已过期报警
		if (me.RegistAlarm.IsRegistAlarm && me.RegistAlarm.HasExpired) {
			appInfos.push(me.RegisterExpiredPanel);
		}
		//注册证将过期报警
		if (me.RegistAlarm.IsRegistAlarm && me.RegistAlarm.HasWillexpire) {
			var willexpireDays = 1;
			if (me.RegistAlarm.WillexpireDays) {
				willexpireDays = parseInt(me.RegistAlarm.WillexpireDays);
			}
			me.RegisterWillExpirePanel.willexpireDays = willexpireDays;
			appInfos.push(me.RegisterWillExpirePanel);
		}

		me.OpenBottleOperGrid = Ext.create('Shell.class.rea.client.openbottleoper.alertinfo.Grid', {
			title: '开瓶未使用完成提示',
			header: true,
			itemId: 'OpenBottleOperGrid'
		});
		appInfos.push(me.OpenBottleOperGrid);
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		return null;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**获取库存预警,效期预警,注册证预警提示信息*/
	getWarning: function(callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;

		JShell.Server.get(url, function(data) {
			if (data.success) {
				var list = (data.value || {}) || [];
				callback(data.value);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});
