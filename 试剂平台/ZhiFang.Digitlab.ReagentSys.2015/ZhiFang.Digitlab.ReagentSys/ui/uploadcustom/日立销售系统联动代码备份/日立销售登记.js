var rlList = me.getComponent('RLSalesRList');
rlList.on({
	afterOpenAddWin : function(formPanel) {
		formPanel.type = 'add';
		formPanel.isAdd();
		formPanel.isSuccessMsg = true;
		formPanel.on({
			default1buttonClick : function(but) {
				var HITACHIBusinessProject_BBusinessStatus_Id = formPanel
						.getComponent('HITACHIBusinessProject_BBusinessStatus_Id');
				HITACHIBusinessProject_BBusinessStatus_Id
						.setValue('4812090953549115417');
				formPanel.submit(but)
			},
			beforeSave : function() {
				var bHITACHICongressId = formPanel
						.getComponent('HITACHIBusinessProject_BHITACHICongress_Id');
				if (bHITACHICongressId) {
					var txtValue = bHITACHICongressId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '日立代表处名称不允许为空');
						return false
					}
				}
				var bCompanyId = formPanel
						.getComponent('HITACHIBusinessProject_BCompany_Id');
				if (bCompanyId) {
					var txtValue = bCompanyId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '经销商名称不允许为空');
						return false
					}
				}
				var bBusinessRouterId = formPanel
						.getComponent('HITACHIBusinessProject_BBusinessRouter_Id');
				var subCompName = formPanel
						.getComponent('HITACHIBusinessProject_SubComp');
				if (bBusinessRouterId) {
					var txtValue = bBusinessRouterId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '销售渠道不允许为空');
						return false
					} else {
						if (txtValue != null && txtValue != undefined) {
							txtValue = txtValue.HITACHIBusinessProject_BBusinessRouter_Id;
							if (txtValue == null || txtValue == ''
									|| txtValue == undefined) {
								Ext.Msg.alert('提示', '销售渠道不允许为空');
								return false
							}
						}
						if (txtValue == '5589674966980227075') {
							if (subCompName) {
								var txtsubCompNameb = subCompName.getValue();
								if (txtsubCompNameb == null
										|| txtsubCompNameb == ''
										|| txtsubCompNameb == undefined) {
									Ext.Msg.alert('提示', '二级代理或销售公司不允许为空');
									return false
								}
							}
						}
					}
				}
				var bRegisterTypeId = formPanel
						.getComponent('HITACHIBusinessProject_BRegisterType_Id');
				if (bRegisterTypeId) {
					var txtValue = bRegisterTypeId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BRegisterType_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '登记类型不允许为空');
							return false
						}
					}
				}
				var bHITACHIClientId = formPanel
						.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
				if (bHITACHIClientId) {
					var txtValue = bHITACHIClientId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '客户名称不允许为空');
						return false
					}
				}
				var askEquipHITACIId = formPanel
						.getComponent('HITACHIBusinessProject_AskEquipHITACI_Id');
				if (askEquipHITACIId) {
					var txtValue = askEquipHITACIId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '询问机型不允许为空');
						return false
					}
				}
				var haveEquip = formPanel
						.getComponent('HITACHIBusinessProject_HaveEquip');
				if (haveEquip) {
					var txtValue = haveEquip.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '现有机型不允许为空');
						return false
					}
				}
				var clinicalLaboratoryDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorName');
				var clinicalLaboratoryDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorPhone');
				var EquipmentdivisionDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorName');
				var EquipmentdivisionDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorPhone');
				var hospitalDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_HospitalDirectorName');
				var hospitalDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_HospitalDirectorPhone');
				if (clinicalLaboratoryDirectorName
						|| EquipmentdivisionDirectorName
						|| hospitalDirectorName) {
					var clinicalLaboratoryDirectorNameValue = clinicalLaboratoryDirectorName
							.getValue();
					var EquipmentdivisionDirectorNameValue = EquipmentdivisionDirectorName
							.getValue();
					var hospitalDirectorNameValue = hospitalDirectorName
							.getValue();
					var txtLaboratoryPhone = clinicalLaboratoryDirectorPhone
							.getValue();
					var txtEquipmentPhone = EquipmentdivisionDirectorPhone
							.getValue();
					var txthospitalDirectorPhone = hospitalDirectorPhone
							.getValue();
					if (clinicalLaboratoryDirectorNameValue != ''
							|| EquipmentdivisionDirectorNameValue != ''
							|| hospitalDirectorNameValue != '') {
						if (clinicalLaboratoryDirectorNameValue != '') {
							if (txtLaboratoryPhone == null
									|| txtLaboratoryPhone == ''
									|| txtLaboratoryPhone == undefined) {
								Ext.Msg.alert('提示', '检验科主任电话不允许为空');
								return false
							}
						}
						if (EquipmentdivisionDirectorNameValue != '') {
							if (txtEquipmentPhone == null
									|| txtEquipmentPhone == ''
									|| txtEquipmentPhone == undefined) {
								Ext.Msg.alert('提示', '设备科科长电话不允许为空');
								return false
							}
						}
						if (hospitalDirectorNameValue != '') {
							if (txthospitalDirectorPhone == null
									|| txthospitalDirectorPhone == ''
									|| txthospitalDirectorPhone == undefined) {
								Ext.Msg.alert('提示', '院长电话不允许为空');
								return false
							}
						}
					} else {
						Ext.Msg.alert('提示', '检验科主任、设备院长、院长三项其中一项必填');
						return false
					}
				}
				var buyNumber = formPanel
						.getComponent('HITACHIBusinessProject_BuyNumber');
				if (buyNumber) {
					var txtValue = buyNumber.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '购买台数不允许为空');
						return false
					}
				}
				var updateTime = formPanel
						.getComponent('HITACHIBusinessProject_UpdateTime');
				var budgetBuyTimeCount = formPanel
						.getComponent('HITACHIBusinessProject_BudgetBuyTimeCount');
				if (updateTime && budgetBuyTimeCount) {
					var updateTimeValue = updateTime.getValue();
					var budgetBuyTimeCountValue = budgetBuyTimeCount.getValue();
					updateTimeValue = updateTimeValue != ''
							? updateTimeValue
							: 0;
					budgetBuyTimeCountValue = budgetBuyTimeCountValue != ''
							? budgetBuyTimeCountValue
							: 0;
					if (updateTimeValue > budgetBuyTimeCountValue) {
						Ext.Msg.alert('提示', '请检查预约签约时间是否 应晚于申请登记日');
						return false
					}
				}
				var bBudgetId = formPanel
						.getComponent('HITACHIBusinessProject_BBudget_Id');
				if (bBudgetId) {
					var txtValue = bBudgetId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BBudget_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '预算状况不允许为空');
							return false
						}
					}
				}
				var bBuyTypeId = formPanel
						.getComponent('HITACHIBusinessProject_BBuyType_Id');
				if (bBuyTypeId) {
					var txtValue = bBuyTypeId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BBuyType_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '购买形式不允许为空');
							return false
						}
					}
				}
				var bFacturerId = formPanel
						.getComponent('HITACHIBusinessProject_BFacturer_Id');
				if (bFacturerId) {
					var txtValue = bFacturerId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '竞争厂家不允许为空');
						return false
					}
				}
				var txtPurpose = formPanel
						.getComponent('HITACHIBusinessProject_Purpose');
				if (txtPurpose) {
					var txtValue = txtPurpose.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '用途不允许为空');
						return false
					}
				}
				var txtWorkStatu = formPanel
						.getComponent('HITACHIBusinessProject_WorkStatus');
				if (txtWorkStatu) {
					var txtValue = txtWorkStatu.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '工作现状不允许为空');
						return false
					}
				}
			},
			saveClick : function() {
				formPanel.close()
			}
		});
		var bfacturerIdCom = formPanel
				.getComponent('HITACHIBusinessProject_BFacturer_Id');
		if (bfacturerIdCom) {
			bfacturerIdCom.on({
				change : function(com, newValue, oldValue, e, eOpts) {
					var nameCom = formPanel
							.getComponent('HITACHIBusinessProject_ContendComp');
					if (nameCom) {
						var value = com.getRawValue();
						nameCom.setValue(value)
					}
				}
			})
		}
		var employeeName1 = Ext.util.Cookies.get('000201');
		var employeeName = getCookie('000201');
		var operaterNameCom = formPanel
				.getComponent('HITACHIBusinessProject_OperaterName');
		if (operaterNameCom) {
			operaterNameCom.setValue(employeeName)
		}
		var updateTimeCom = formPanel
				.getComponent('HITACHIBusinessProject_UpdateTime');
		if (updateTimeCom) {
			var valnew = new Date();
			var c = function(datavalue) {
				var val = datavalue;
				updateTimeCom.setValue(val)
			};
			getServerInformation(c)
		}
		var congressId = formPanel
				.getComponent('HITACHIBusinessProject_BHITACHICongress_Id');
		if (congressId) {
			congressId.on({
				change : function(com, newValue, oldValue, e, eOpts) {
					var congressName = formPanel
							.getComponent('HITACHIBusinessProject_CongressName');
					if (congressName) {
						var value = com.getRawValue();
						congressName.setValue(value)
					}
				}
			})
		}
		var bCompanyId = formPanel
				.getComponent('HITACHIBusinessProject_BCompany_Id');
		if (bCompanyId) {
			bCompanyId.on({
		     select : function(com, records, eOpts) {
                    var newValue=com.getValue();
                    if (newValue!='') {
					var url = getRootPath()
							+ '/HITACHIService.svc/ST_UDTO_SearchBCompanyByHQL?isPlanish=true&fields=BCompany_Name,BCompany_Phone,BCompany_Fax,BCompany_Address,BCompany_PostCode,BCompany_LinkMan,BCompany_LinkTel,BCompany_RequestMan,BCompany_RequestTel,BCompany_OperaterName,BCompany_UpdateTime,BCompany_OperateId,BCompany_SName,BCompany_Shortcode,BCompany_PinYinZiTou,BCompany_Comment,BCompany_IsUse,BCompany_CompUrl,BCompany_Id,BCompany_LabID,BCompany_DataAddTime,BCompany_DataTimeStamp&page=1&start=0&limit=10000';
					var fields = 'BCompany_BHITACHICongress_Id,BCompany_BHITACHICongress_Name';
					var w = '&where=bcompany.Id=' + newValue;
					url = url + w;
					var callback = function(responseText) {
						var result = Ext.JSON.decode(responseText);
						if (result.success) {
							if (result.ResultDataValue
									&& result.ResultDataValue != '') {
								var r = Ext.JSON.decode(result.ResultDataValue);
								var BCompanyPhone = r.list[0]['BCompany_Phone'];
								var fax = r.list[0]['BCompany_Fax'];
								var phoneCom = formPanel
										.getComponent('HITACHIBusinessProject_CompPhone');
								if (phoneCom) {
									phoneCom.setValue(BCompanyPhone)
								}
								var faxCom = formPanel
										.getComponent('HITACHIBusinessProject_CompFax');
								if (faxCom) {
									faxCom.setValue(fax)
								}
							}
						}
					};
					getToServer(url, callback);
					var bCompanyName = formPanel
							.getComponent('HITACHIBusinessProject_CompName');
					if (bCompanyName) {
						var value = com.getRawValue();
						bCompanyName.setValue(value)
					}
                }
				}
			})
		}
		var clientId = formPanel
				.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
		if (clientId) {
			clientId.on({
				  change : function(com,newValue) {
                    if (newValue!='') {
					var urlHIClient = getRootPath()
							+ '/HITACHIService.svc/ST_UDTO_SearchBHITACHIClientByHQL?isPlanish=true&fields=BHITACHIClient_Id,BHITACHIClient_Name,BHITACHIClient_Address,BHITACHIClient_Postcode,BHITACHIClient_BedCount,BHITACHIClient_ClinicalLaboratoryDirectorName,BHITACHIClient_ClinicalLaboratoryDirectorPhone,BHITACHIClient_EquipmentdivisionDirectorName,BHITACHIClient_EquipmentdivisionDirectorPhone,BHITACHIClient_HospitalDirectorName,BHITACHIClient_HospitalDirectorPhone';
					var fields = ',BHITACHIClient_BLabType_Name,BHITACHIClient_BLabType_Id,BHITACHIClient_BLabType_DataTimeStamp';
					urlHIClient = urlHIClient + fields
							+ '&page=1&start=0&limit=10000';
					var w = '&where=bhitachiclient.Id=' + newValue;
					urlHIClient = urlHIClient + w;
					var callbackurlHIClient = function(responseText) {
						var result = Ext.JSON.decode(responseText);
						if (result.success) {
							if (result.ResultDataValue
									&& result.ResultDataValue != '') {
								var r = Ext.JSON.decode(result.ResultDataValue);
								var nameValue = r.list[0]['BHITACHIClient_Name'];
								var address = r.list[0]['BHITACHIClient_Address'];
								var postcode = r.list[0]['BHITACHIClient_Postcode'];
								var bedCount = r.list[0]['BHITACHIClient_BedCount'];
								var nameCom = formPanel
										.getComponent('HITACHIBusinessProject_BHITACHIClient_Name');
								if (nameCom) {
									nameCom.setValue(nameValue)
								}
								var addressCom = formPanel
										.getComponent('HITACHIBusinessProject_HITACHIClientAddress');
								if (addressCom) {
									addressCom.setValue(address)
								}
								var postcodeCom = formPanel
										.getComponent('HITACHIBusinessProject_HITACHIClientPostcode');
								if (postcodeCom) {
									postcodeCom.setValue(bedCount)
								}
								var bedCountCom = formPanel
										.getComponent('HITACHIBusinessProject_HITACHIClientBedCount');
								if (bedCountCom) {
									bedCountCom.setValue(bedCount)
								}
								var cldirectorName = r.list[0]['BHITACHIClient_ClinicalLaboratoryDirectorName'];
								var cldirectorNameCom = formPanel
										.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorName');
								if (cldirectorNameCom) {
									cldirectorNameCom.setValue(cldirectorName)
								}
								var cldirectorPhone = r.list[0]['BHITACHIClient_ClinicalLaboratoryDirectorPhone'];
								var cldirectorPhoneCom = formPanel
										.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorPhone');
								if (cldirectorPhoneCom) {
									cldirectorPhoneCom
											.setValue(cldirectorPhone)
								}
								var dDirectorName = r.list[0]['BHITACHIClient_EquipmentdivisionDirectorName'];
								var dDirectorNameCom = formPanel
										.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorName');
								if (dDirectorNameCom) {
									dDirectorNameCom.setValue(dDirectorName)
								}
								var dDirectorPhone = r.list[0]['BHITACHIClient_EquipmentdivisionDirectorPhone'];
								var dDirectorPhoneCom = formPanel
										.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorPhone');
								if (dDirectorPhoneCom) {
									dDirectorPhoneCom.setValue(dDirectorPhone)
								}
								var hdName = r.list[0]['BHITACHIClient_HospitalDirectorName'];
								var hdNameCom = formPanel
										.getComponent('HITACHIBusinessProject_HospitalDirectorName');
								if (hdNameCom) {
									hdNameCom.setValue(hdName)
								}
								var hdPhone = r.list[0]['BHITACHIClient_HospitalDirectorPhone'];
								var hdPhoneCom = formPanel
										.getComponent('HITACHIBusinessProject_HospitalDirectorPhone');
								if (hdPhoneCom) {
									hdPhoneCom.setValue(hdPhone)
								}
								var bLabTypeName = r.list[0]['BHITACHIClient_BLabType_Name'];
								var bLabTypeNameCom = formPanel
										.getComponent('HITACHIBusinessProject_HITACHIClientTypeName');
								if (bLabTypeNameCom) {
									bLabTypeNameCom.setValue(bLabTypeName)
								}
							}
						}
					};
					getToServer(urlHIClient, callbackurlHIClient);
					var clientName = formPanel
							.getComponent('HITACHIBusinessProject_HITACHIClientName');
					if (clientName) {
						var value = com.getRawValue();
						clientName.setValue(value)
					}
                }
				}
			})
		}
	},
	afterOpenShowWin : function(formPanel) {
		formPanel.type = 'show';
		formPanel.isSuccessMsg == true;
		var callbackShow = function() {
			var clientId = formPanel
					.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
			var idValue = clientId.getValue();
			var clientName = formPanel
					.getComponent('HITACHIBusinessProject_HITACHIClientName');
			var clientNameValue = clientName.getValue();
			clientId.store.loadData([[idValue, clientNameValue]]);
			clientId.setValue(idValue)
		};
		formPanel.isShow(formPanel.dataId, callbackShow)
	}
});
rlList.on({
	afterOpenEditWin : function(formPanel) {
		formPanel.on({
			default1buttonClick : function(but) {
				var HITACHIBusinessProject_BBusinessStatus_Id = formPanel
						.getComponent('HITACHIBusinessProject_BBusinessStatus_Id');
				HITACHIBusinessProject_BBusinessStatus_Id
						.setValue('4812090953549115417');
				var BHITACHICongress_Id = formPanel
						.getComponent('HITACHIBusinessProject_BCompany_BHITACHICongress_Id');
				if (BHITACHICongress_Id && BHITACHICongress_Id != undefined) {
					BHITACHICongress_Id.setValue('')
				}
				var clientId1 = formPanel
						.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
				var idValue1 = clientId1.getValue();
				formPanel.submit(but)
			},
			beforeSave : function() {
				var bHITACHICongressId = formPanel
						.getComponent('HITACHIBusinessProject_BHITACHICongress_Id');
				if (bHITACHICongressId) {
					var txtValue = bHITACHICongressId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '日立代表处名称不允许为空');
						return false
					}
				}
				var bCompanyId = formPanel
						.getComponent('HITACHIBusinessProject_BCompany_Id');
				if (bCompanyId) {
					var txtValue = bCompanyId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '经销商名称不允许为空');
						return false
					}
				}
				var bBusinessRouterId = formPanel
						.getComponent('HITACHIBusinessProject_BBusinessRouter_Id');
				var subCompName = formPanel
						.getComponent('HITACHIBusinessProject_SubComp');
				if (bBusinessRouterId) {
					var txtValue = bBusinessRouterId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '销售渠道不允许为空');
						return false
					} else {
						if (txtValue != null && txtValue != undefined) {
							txtValue = txtValue.HITACHIBusinessProject_BBusinessRouter_Id;
							if (txtValue == null || txtValue == ''
									|| txtValue == undefined) {
								Ext.Msg.alert('提示', '销售渠道不允许为空');
								return false
							}
						}
						if (txtValue == '5589674966980227075') {
							if (subCompName) {
								var txtsubCompNameb = subCompName.getValue();
								if (txtsubCompNameb == null
										|| txtsubCompNameb == ''
										|| txtsubCompNameb == undefined) {
									Ext.Msg.alert('提示', '二级代理或销售公司不允许为空');
									return false
								}
							}
						}
					}
				}
				var bRegisterTypeId = formPanel
						.getComponent('HITACHIBusinessProject_BRegisterType_Id');
				if (bRegisterTypeId) {
					var txtValue = bRegisterTypeId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BRegisterType_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '登记类型不允许为空');
							return false
						}
					}
				}
				var bHITACHIClientId = formPanel
						.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
				if (bHITACHIClientId) {
					var txtValue = bHITACHIClientId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '客户名称不允许为空');
						return false
					}
				}
				var askEquipHITACIId = formPanel
						.getComponent('HITACHIBusinessProject_AskEquipHITACI_Id');
				if (askEquipHITACIId) {
					var txtValue = askEquipHITACIId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '询问机型不允许为空');
						return false
					}
				}
				var haveEquip = formPanel
						.getComponent('HITACHIBusinessProject_HaveEquip');
				if (haveEquip) {
					var txtValue = haveEquip.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '现有机型不允许为空');
						return false
					}
				}
				var clinicalLaboratoryDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorName');
				var clinicalLaboratoryDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorPhone');
				var EquipmentdivisionDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorName');
				var EquipmentdivisionDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorPhone');
				var hospitalDirectorName = formPanel
						.getComponent('HITACHIBusinessProject_HospitalDirectorName');
				var hospitalDirectorPhone = formPanel
						.getComponent('HITACHIBusinessProject_HospitalDirectorPhone');
				if (clinicalLaboratoryDirectorName
						|| EquipmentdivisionDirectorName
						|| hospitalDirectorName) {
					var clinicalLaboratoryDirectorNameValue = clinicalLaboratoryDirectorName
							.getValue();
					var EquipmentdivisionDirectorNameValue = EquipmentdivisionDirectorName
							.getValue();
					var hospitalDirectorNameValue = hospitalDirectorName
							.getValue();
					var txtLaboratoryPhone = clinicalLaboratoryDirectorPhone
							.getValue();
					var txtEquipmentPhone = EquipmentdivisionDirectorPhone
							.getValue();
					var txthospitalDirectorPhone = hospitalDirectorPhone
							.getValue();
					if (clinicalLaboratoryDirectorNameValue != ''
							|| EquipmentdivisionDirectorNameValue != ''
							|| hospitalDirectorNameValue != '') {
						if (clinicalLaboratoryDirectorNameValue != '') {
							if (txtLaboratoryPhone == null
									|| txtLaboratoryPhone == ''
									|| txtLaboratoryPhone == undefined) {
								Ext.Msg.alert('提示', '检验科主任电话不允许为空');
								return false
							}
						}
						if (EquipmentdivisionDirectorNameValue != '') {
							if (txtEquipmentPhone == null
									|| txtEquipmentPhone == ''
									|| txtEquipmentPhone == undefined) {
								Ext.Msg.alert('提示', '设备科科长电话不允许为空');
								return false
							}
						}
						if (hospitalDirectorNameValue != '') {
							if (txthospitalDirectorPhone == null
									|| txthospitalDirectorPhone == ''
									|| txthospitalDirectorPhone == undefined) {
								Ext.Msg.alert('提示', '院长电话不允许为空');
								return false
							}
						}
					} else {
						Ext.Msg.alert('提示', '检验科主任、设备院长、院长三项其中一项必填');
						return false
					}
				}
				var buyNumber = formPanel
						.getComponent('HITACHIBusinessProject_BuyNumber');
				if (buyNumber) {
					var txtValue = buyNumber.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '购买台数不允许为空');
						return false
					}
				}
				var updateTime = formPanel
						.getComponent('HITACHIBusinessProject_UpdateTime');
				var budgetBuyTimeCount = formPanel
						.getComponent('HITACHIBusinessProject_BudgetBuyTimeCount');
				if (updateTime && budgetBuyTimeCount) {
					var updateTimeValue = updateTime.getValue();
					var budgetBuyTimeCountValue = budgetBuyTimeCount.getValue();
					updateTimeValue = updateTimeValue != ''
							? updateTimeValue
							: 0;
					budgetBuyTimeCountValue = budgetBuyTimeCountValue != ''
							? budgetBuyTimeCountValue
							: 0;
					if (updateTimeValue > budgetBuyTimeCountValue) {
						Ext.Msg.alert('提示', '请检查预约签约时间是否 应晚于申请登记日');
						return false
					}
				}
				var bBudgetId = formPanel
						.getComponent('HITACHIBusinessProject_BBudget_Id');
				if (bBudgetId) {
					var txtValue = bBudgetId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BBudget_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '预算状况不允许为空');
							return false
						}
					}
				}
				var bBuyTypeId = formPanel
						.getComponent('HITACHIBusinessProject_BBuyType_Id');
				if (bBuyTypeId) {
					var txtValue = bBuyTypeId.getValue();
					if (txtValue != null && txtValue != undefined) {
						txtValue = txtValue.HITACHIBusinessProject_BBuyType_Id;
						if (txtValue == null || txtValue == ''
								|| txtValue == undefined) {
							Ext.Msg.alert('提示', '购买形式不允许为空');
							return false
						}
					}
				}
				var bFacturerId = formPanel
						.getComponent('HITACHIBusinessProject_BFacturer_Id');
				if (bFacturerId) {
					var txtValue = bFacturerId.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '竞争厂家不允许为空');
						return false
					}
				}
				var txtPurpose = formPanel
						.getComponent('HITACHIBusinessProject_Purpose');
				if (txtPurpose) {
					var txtValue = txtPurpose.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '用途不允许为空');
						return false
					}
				}
				var txtWorkStatu = formPanel
						.getComponent('HITACHIBusinessProject_WorkStatus');
				if (txtWorkStatu) {
					var txtValue = txtWorkStatu.getValue();
					if (txtValue == null || txtValue == ''
							|| txtValue == undefined) {
						Ext.Msg.alert('提示', '工作现状不允许为空');
						return false
					}
				}
			}
		});
		formPanel.type = 'edit';
		formPanel.isSuccessMsg == true;
		var employeeName2 = Ext.util.Cookies.get('000201');
		var employeeName = getCookie('000201');
		getServerLists = function(url, hqlWhere, async) {
			var arrLists = [];
			var myUrl = '';
			if (hqlWhere && hqlWhere != null) {
				myUrl = url + '&where=' + hqlWhere
			} else {
				myUrl = url
			}
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async : async,
				url : myUrl,
				method : 'GET',
				success : function(response, opts) {
					var data = Ext.JSON.decode(response.responseText);
					var success = (data.success + '' == 'true' ? true : false);
					if (!success) {
						alert(data.ErrorInfo)
					}
					if (success) {
						if (data.ResultDataValue && data.ResultDataValue != '') {
							var ResultDataValue = Ext.JSON
									.decode(data.ResultDataValue);
							arrLists = ResultDataValue.list;
						} else {
							arrLists = []
						}
					}
				},
				failure : function(response, options) {
					arrLists = []
				}
			});
			return arrLists
		};
		var callback = function() {
			var bfacturerId1 = formPanel
					.getComponent('HITACHIBusinessProject_BFacturer_Id');
			var fieldsName = 'BFacturer_Name,BFacturer_Id,BFacturer_DataTimeStamp,BFacturer_IsUse,BFacturer_DispOrder';
			var url = 'HITACHIService.svc/ST_UDTO_SearchBFacturerByHQL?isPlanish=true&sort=[{%27property%27:%27BFacturer_DispOrder%27,%27direction%27:%27ASC%27}]';
			var url2 = 'HITACHIService.svc/ST_UDTO_SearchBFacturerByHQL?isPlanish=true&sort=[{%27property%27:%27BFacturer_DispOrder%27,%27direction%27:%27ASC%27}]';
			var myurl = getRootPath()
					+ '/'
					+ url2
					+ '&fields='
					+ 'BFacturer_Name,BFacturer_Id,BFacturer_DataTimeStamp,BFacturer_IsUse,BFacturer_DispOrder';
			var Hqlwhere = 'bfacturer.IsUse=1';
			var dataLists = getServerLists(myurl, Hqlwhere, false);
			bfacturerId1.store.loadData(dataLists);
			var clientName = formPanel
					.getComponent('HITACHIBusinessProject_HITACHIClientName');
			var clientNameValue = clientName.getValue();
			var operaterNameCom = formPanel
					.getComponent('HITACHIBusinessProject_OperaterName');
			if (operaterNameCom) {
				operaterNameCom.setValue(employeeName)
			}
			var updateTimeCom = formPanel
					.getComponent('HITACHIBusinessProject_UpdateTime');
			if (updateTimeCom) {
				var valnew = new Date();
				var c = function(datavalue) {
					var val = datavalue;
					updateTimeCom.setValue(val)
				};
				getServerInformation(c)
			}
			var bfacturerId = formPanel
					.getComponent('HITACHIBusinessProject_BFacturer_Id');
			if (bfacturerId) {
				bfacturerId.on({
					change : function(com, newValue, oldValue, e, eOpts) {
						var nameCom = formPanel
								.getComponent('HITACHIBusinessProject_ContendComp');
						if (nameCom) {
							var value = com.getRawValue();
							nameCom.setValue(value)
						}
					}
				})
			}
			var congressId = formPanel
					.getComponent('HITACHIBusinessProject_BCompany_BHITACHICongress_Id');
			if (congressId) {
				congressId.on({
					change : function(com, newValue, oldValue, e, eOpts) {
						var congressName = formPanel
								.getComponent('HITACHIBusinessProject_CongressName');
						if (congressName) {
							var value = com.getRawValue();
							congressName.setValue(value)
						}
					}
				})
			}
			var bCompanyId = formPanel
					.getComponent('HITACHIBusinessProject_BCompany_Id');
			if (bCompanyId) {
				bCompanyId.on({
				select : function(com, records, eOpts) {
                    var newValue=com.getValue();
                    if (newValue!='') {
						var url = getRootPath()
								+ '/HITACHIService.svc/ST_UDTO_SearchBCompanyByHQL?isPlanish=true&fields=BCompany_Name,BCompany_Phone,BCompany_Fax,BCompany_Address,BCompany_PostCode,BCompany_LinkMan,BCompany_LinkTel,BCompany_RequestMan,BCompany_RequestTel,BCompany_OperaterName,BCompany_UpdateTime,BCompany_OperateId,BCompany_SName,BCompany_Shortcode,BCompany_PinYinZiTou,BCompany_Comment,BCompany_IsUse,BCompany_CompUrl,BCompany_Id,BCompany_LabID,BCompany_DataAddTime,BCompany_DataTimeStamp&page=1&start=0&limit=10000';
						var fields = 'BCompany_BHITACHICongress_Id,BCompany_BHITACHICongress_Name';
						var w = '&where=bcompany.Id=' + newValue;
						url = url + w;
						var callback = function(responseText) {
							var result = Ext.JSON.decode(responseText);
							if (result.success) {
								if (result.ResultDataValue
										&& result.ResultDataValue != '') {
									var r = Ext.JSON
											.decode(result.ResultDataValue);
									var BCompanyPhone = r.list[0]['BCompany_Phone'];
									var fax = r.list[0]['BCompany_Fax'];
									var phoneCom = formPanel
											.getComponent('HITACHIBusinessProject_CompPhone');
									if (phoneCom) {
										phoneCom.setValue(BCompanyPhone)
									}
									var faxCom = formPanel
											.getComponent('HITACHIBusinessProject_CompFax');
									if (faxCom) {
										faxCom.setValue(fax)
									}
								}
							}
						};
						getToServer(url, callback);
						var bCompanyName = formPanel
								.getComponent('HITACHIBusinessProject_CompName');
						if (bCompanyName) {
							var value = com.getRawValue();
							bCompanyName.setValue(value)
						}
                    }
					}
				})
			}
			var clientId = formPanel
					.getComponent('HITACHIBusinessProject_BHITACHIClient_Id');
			var idValue = clientId.getValue();
			clientId.store.loadData([[idValue, clientNameValue]]);
			clientId.setValue(idValue);
			if (clientId) {
				clientId.on({
			    change : function(com,newValue) {
                    if (newValue!='') {
						var urlHIClient = getRootPath()
								+ '/HITACHIService.svc/ST_UDTO_SearchBHITACHIClientByHQL?isPlanish=true&fields=BHITACHIClient_Id,BHITACHIClient_Name,BHITACHIClient_Address,BHITACHIClient_Postcode,BHITACHIClient_BedCount,BHITACHIClient_ClinicalLaboratoryDirectorName,BHITACHIClient_ClinicalLaboratoryDirectorPhone,BHITACHIClient_EquipmentdivisionDirectorName,BHITACHIClient_EquipmentdivisionDirectorPhone,BHITACHIClient_HospitalDirectorName,BHITACHIClient_HospitalDirectorPhone';
						var fields = ',BHITACHIClient_BLabType_Name,BHITACHIClient_BLabType_Id,BHITACHIClient_BLabType_DataTimeStamp';
						urlHIClient = urlHIClient + fields
								+ '&page=1&start=0&limit=10000';
						var w = '&where=bhitachiclient.Id=' + newValue;
						urlHIClient = urlHIClient + w;
						var callbackurlHIClient = function(responseText) {
							var result = Ext.JSON.decode(responseText);
							if (result.success) {
								if (result.ResultDataValue
										&& result.ResultDataValue != '') {
									var r = Ext.JSON
											.decode(result.ResultDataValue);
									var nameValue = r.list[0]['BHITACHIClient_Name'];
									var address = r.list[0]['BHITACHIClient_Address'];
									var postcode = r.list[0]['BHITACHIClient_Postcode'];
									var bedCount = r.list[0]['BHITACHIClient_BedCount'];
									var nameCom = formPanel
											.getComponent('HITACHIBusinessProject_HITACHIClientName');
									if (nameCom) {
										nameCom.setValue(nameValue)
									}
									var addressCom = formPanel
											.getComponent('HITACHIBusinessProject_HITACHIClientAddress');
									if (addressCom) {
										addressCom.setValue(address)
									}
									var postcodeCom = formPanel
											.getComponent('HITACHIBusinessProject_HITACHIClientPostcode');
									if (postcodeCom) {
										postcodeCom.setValue(bedCount)
									}
									var bedCountCom = formPanel
											.getComponent('HITACHIBusinessProject_HITACHIClientBedCount');
									if (bedCountCom) {
										bedCountCom.setValue(bedCount)
									}
									var cldirectorName = r.list[0]['BHITACHIClient_ClinicalLaboratoryDirectorName'];
									var cldirectorNameCom = formPanel
											.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorName');
									if (cldirectorNameCom) {
										cldirectorNameCom
												.setValue(cldirectorName)
									}
									var cldirectorPhone = r.list[0]['BHITACHIClient_ClinicalLaboratoryDirectorPhone'];
									var cldirectorPhoneCom = formPanel
											.getComponent('HITACHIBusinessProject_ClinicalLaboratoryDirectorPhone');
									if (cldirectorPhoneCom) {
										cldirectorPhoneCom
												.setValue(cldirectorPhone)
									}
									var dDirectorName = r.list[0]['BHITACHIClient_EquipmentdivisionDirectorName'];
									var dDirectorNameCom = formPanel
											.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorName');
									if (dDirectorNameCom) {
										dDirectorNameCom
												.setValue(dDirectorName)
									}
									var dDirectorPhone = r.list[0]['BHITACHIClient_EquipmentdivisionDirectorPhone'];
									var dDirectorPhoneCom = formPanel
											.getComponent('HITACHIBusinessProject_EquipmentdivisionDirectorPhone');
									if (dDirectorPhoneCom) {
										dDirectorPhoneCom
												.setValue(dDirectorPhone)
									}
									var hdName = r.list[0]['BHITACHIClient_HospitalDirectorName'];
									var hdNameCom = formPanel
											.getComponent('HITACHIBusinessProject_HospitalDirectorName');
									if (hdNameCom) {
										hdNameCom.setValue(hdName)
									}
									var hdPhone = r.list[0]['BHITACHIClient_HospitalDirectorPhone'];
									var hdPhoneCom = formPanel
											.getComponent('HITACHIBusinessProject_HospitalDirectorPhone');
									if (hdPhoneCom) {
										hdPhoneCom.setValue(hdPhone)
									}
									var bLabTypeName = r.list[0]['BHITACHIClient_BLabType_Name'];
									var bLabTypeNameCom = formPanel
											.getComponent('HITACHIBusinessProject_HITACHIClientTypeName');
									if (bLabTypeNameCom) {
										bLabTypeNameCom.setValue(bLabTypeName)
									}
								}
							}
						};
						getToServer(urlHIClient, callbackurlHIClient)
                    }
					}
				})
			}
		};
		formPanel.isEdit(formPanel.dataId, callback)
	}
});
