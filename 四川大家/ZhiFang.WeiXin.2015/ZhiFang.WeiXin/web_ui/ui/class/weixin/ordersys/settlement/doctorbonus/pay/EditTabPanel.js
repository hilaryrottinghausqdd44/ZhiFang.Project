/**
 * 检查并打款
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.pay.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	isUpdateOSDoctorBonusFormStatus: false,
	title: '检查并打款',
	/**是否发放方式按钮*/
	hiddenPaymentMethod: false,
	/**通过按钮显示文字*/
	btnPassText: "打款",
	/**是否隐藏退回按钮*/
	//hiddenRetract: true,
	BonusGridClass: 'Shell.class.weixin.ordersys.settlement.doctorbonus.pay.EditBonusGrid',

	/**批量选择医生奖金记录的检查并打款操作处理服务地址*/
	editOSDoctorBonusUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusListPayStatus',
	/**检查奖金记录里是否还有未打款*/
	checkUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCheckIsUpdatePayed',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.title = "检查并打款";
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.BonusGrid.on({
			//发放处理
			onDoctorBonusPayOne: function(grid, record) {
				var records = [];
				records.push(record);
				me.Status = "9";
				me.OperationMemo = "检查并打款(微信发放)";
				me.updateOSDoctorBonusPay(records);
			}
		});
	},
	/**检查并打款方法*/
	onPassClick: function() {
		var me = this;
		me.Status = "9";
		me.OperationMemo = "检查并打款";
		var records = me.BonusGrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.updateOSDoctorBonusPay(records);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		me.Status = "10";
		me.OperationMemo = "检查并打款退回";
		me.updateStatus();
	},
	/**检查并打款*/
	updateOSDoctorBonusPay: function(records) {
		var me = this;
		if(!records) {
			records = me.BonusGrid.getSelectionModel().getSelection();
		}
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var updateArr = [];
		for(var i in records) {
			var record = records[i];
			var status = "" + record.get("Status");
			//检查并打款,打款完成
			if(status != "9" && status != "11") {
				var orderFormAmount = 0,
					percent = 0,
					amount = 0;
				orderFormAmount = record.get("OrderFormAmount");
				amount = record.get("Amount");
				percent = record.get("Percent");
				
				if(orderFormAmount) orderFormAmount = parseFloat(orderFormAmount);
				if(amount) amount = parseFloat(amount);
				if(percent) percent = parseFloat(percent);

				if(orderFormAmount > 0 && amount > 0 && percent <= 0) {
					percent = (parseFloat(amount / orderFormAmount) * 100).toFixed(2);
					record.set('Percent', percent);
				}
				if(orderFormAmount > 0 && percent > 0 && amount <= 0) {
					amount = parseFloat(orderFormAmount* percent * 0.01).toFixed(2);
					record.set('Amount', amount);
				}

				if(amount > 0) {
					record.data.Amount = amount;
					updateArr.push(record);
				}
			}
		}
		if(updateArr.length == 0) {
			JShell.Msg.error("没有需要打款的奖金记录!");
			return;
		}
		//发放方式验证		
		for(var i in updateArr) {
			var paymentMethod = "" + updateArr[i].get("PaymentMethod");
			if(!paymentMethod) {
				JShell.Msg.error("请选择打款的发放方式后再操作!");
				return;
			}
		}
		//发放总金额与发放的微信帐号总金额验证

		var applyInfo = {
			OSDoctorBonusForm: {
				"Id": me.PK,
				"Status": me.Status
			},
			OSDoctorBonus: null,
			OperationMemo: me.OperationMemo
		};
		me.showMask("数据提交保存中...");
		var index = 0;
		me.updateErrorCount = 0;
		me.updateCount = 0;
		me.updateLength = updateArr.length;

		for(var i in updateArr) {
			var record = me.dealWithData(updateArr[i]);
			var oldStatus = record.get("Status");
			applyInfo.OSDoctorBonus = record.data;
			applyInfo.OSDoctorBonus.Status = me.Status;
			var params = {
				entity: applyInfo
			};
			me.updatePayOfOne(index, params, record, oldStatus);
			index++;
		}
	},
	updatePayOfOne: function(index, params, record, oldStatus) {
		var me = this;
		params = Ext.JSON.encode(params);
		var url = (me.editOSDoctorBonusUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editOSDoctorBonusUrl;
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					me.updateCount++;
					//状态更新:检查并打款(9)
					record.set(me.BonusGrid.DelField, true);
					record.set('Status', me.Status);
					record.commit();
				} else {
					me.hideMask();
					me.updateErrorCount++;
					record.set(me.BonusGrid.DelField, false);
					record.set('Status', oldStatus);
					record.set('ErrorInfo', data.msg);
				}
				if(me.updateCount + me.updateErrorCount == me.updateLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.updateErrorCount == 0) {
						me.searchCheckIsUpdatePayed();
						//me.BonusGrid.onSearch();
					} else {
						JShell.Msg.error(me.OperationMemo + "操作失败!<br />" + '具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**检查后台数据,以确认是否都全部打款完成*/
	searchCheckIsUpdatePayed: function() {
		var me = this;
		var isUpdateAll = true;
		//先检查前台数据,再检查后台数据
		me.BonusGrid.store.each(function(record) {
			var status = "" + record.get("Status");
			//检查并打款,打款完成
			if(status == "9" || status == "11" || status == "12") {
				isUadateAll = false;
			}
		});
		//检验后台数据
		if(isUpdateAll == true) {
			var url = (me.checkUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.checkUrl;
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + me.PK;
			JShell.Server.get(url, function(data) {
				isUpdateAll = data.success;
				if(data.success) {
					isUpdateAll = data.success;
				}
			}, false);
		}
		if(isUpdateAll == true) {
			me.Status = "11";
			me.OperationMemo = "打款完成";
			me.updateStatus();
		}
	}
});