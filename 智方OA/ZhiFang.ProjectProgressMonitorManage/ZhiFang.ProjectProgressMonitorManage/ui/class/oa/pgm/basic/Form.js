/**
 * 程序发布表单
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.pgm.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 210,
		labelAlign: 'right'
	},
	/*程序类型:1为通用;2为定制通讯;3为通讯模板*/
	PROGRAMTYPE: '1',
	//programType:'',
	formtype: "add",
	title: '程序信息',
	width: 760,
	height: 565,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**获取数据服务路径(编辑时不需要更新总阅读数)*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramById?isPlanish=true&isUpdateCounts=false',
	/**新增服务地址*/
	addUrl: '/PDProgramManageService.svc/PGM_UDTO_AddPGMProgramByFormData',
	/**修改服务地址*/
	editUrl: '/PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgramByFieldAndFormData',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**所属字典树上级节点Id*/
	PBDictTreeId: "",
	PBDictTreeCName: '',
	/**列表行的树类型Id*/
	SubBDictTreeId: "",
	/**列表行的树类型*/
	SubBDictTreeCName: "",
	autoScroll: true,
	PK: '',
	ETYPEID: '',
	titleEmptyText: '必填项', //  命名规则(通讯-分类-厂商-型号-程序名-版本号)
	/*程序选择(通用的通讯程序:单向通用,双向通用)时过滤*/
	OriginalDefaultWhere: 'pgmprogram.SubBDictTree.Id in(4718339838315108325,5289247771696530773)',
	/**当前树选择的节点是否是叶子节点*/
	isLeaf: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		var width = me.width / 4 - 10;
		me.defaults.width = (width < 180) ? 180 : width;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createPGMProgram_NewFileName("程序名称");
		me.createPGMProgram_Title("标题");
		me.createPGMProgram_No('编号');
		me.createPGMProgram_VersionNo("版本号");
		//通用程序授权号
		me.createPGMProgram_SQH("授权号");
		
		//me.createPGMProgram_IsUse('是否使用');
		me.createPGMProgram_Keyword('关键字');
		me.createPGMProgram_SubBDictTree_CName("分类");
		me.createPGMProgram_ClientName("用户选择");
		me.createPGMProgram_OtherFactoryName("厂家选择");

		me.createPGMProgram_BEquip_CName("仪器选择");
		me.createPGMProgram_OriginalPGMProgram_Title("程序选择");
		me.createPGMProgram_File("附件信息");
		me.createPGMProgram_FileName("原附件信息");
		me.createPGMProgram_DispOrder("显示次序");
		//me.createPGMProgram_PublisherDateTime("发布日期");
		me.createPGMProgram_IsDiscuss("是否可评论");
		me.createPGMProgram_Memo('概要');
	},
	/*
	 * 程序名称,自动生成
	 * 声称规则：父节点-子节点-分类-厂商-型号-程序名-版本号
	 * */
	createPGMProgram_NewFileName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_NewFileName = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'PGMProgram_NewFileName',
			itemId: 'PGMProgram_NewFileName'
		};
	},
	createPGMProgram_DispOrder: function(fieldLabel) {
		var me = this;
		me.PGMProgram_DispOrder = {
			xtype: 'numberfield',
			fieldLabel: fieldLabel,
			name: 'PGMProgram_DispOrder'
		};
	},
	/*用户(医院)选择***/
	createPGMProgram_ClientName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_ClientName = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_ClientName',
			itemId: 'PGMProgram_ClientName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.oa.pgm.basic.PClientCheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 420,
				/**默认加载*/
				defaultLoad: true,
				defaultWhere: "pclient.ClientTypeID=5437113536004455761"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('PGMProgram_ClientName');
					var Id = me.getComponent('PGMProgram_ClientID');
					CName.setValue(record ? record.get('PClient_Name') : '');
					Id.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			}
		};
	},
	/*厂家选择***/
	createPGMProgram_OtherFactoryName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_OtherFactoryName = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_OtherFactoryName',
			itemId: 'PGMProgram_OtherFactoryName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.oa.pgm.basic.PClientCheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 420,
				/**默认加载*/
				defaultLoad: true,
				//类型为仪器厂商
				defaultWhere: "pclient.ClientTypeID=5431102606008085877"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('PGMProgram_OtherFactoryName');
					var Id = me.getComponent('PGMProgram_OtherFactoryID');
					CName.setValue(record ? record.get('PClient_Name') : '');
					Id.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			}
		};
	},

	/*仪器选择***/
	createPGMProgram_BEquip_CName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_BEquip_CName = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_BEquip_CName',
			itemId: 'PGMProgram_BEquip_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.bequip.CheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 480
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('PGMProgram_BEquip_CName');
					var Id = me.getComponent('PGMProgram_BEquip_Id');
					CName.setValue(record ? record.get('BEquip_CName') : '');
					Id.setValue(record ? record.get('BEquip_Id') : '');

					var EquipType = me.getComponent("PGMProgram_EquipType_CName");
					var EquipFactoryBrand = me.getComponent("PGMProgram_BEquip_EquipFactoryBrand_CName");
					var Equipversion = me.getComponent("PGMProgram_BEquip_Equipversion");

					EquipType.setValue(record ? record.get('BEquip_EquipType_CName') : '');
					EquipFactoryBrand.setValue(record ? record.get('BEquip_EquipFactoryBrand_CName') : '');
					Equipversion.setValue(record ? record.get('BEquip_Equipversion') : '');

					me.changeNewFileNameValue();
					p.close();
				}
			}
		};
	},
	/*父程序选择***/
	createPGMProgram_OriginalPGMProgram_Title: function(fieldLabel) {
		var me = this;
		me.PGMProgram_OriginalPGMProgram_Title = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_OriginalPGMProgram_Title',
			itemId: 'PGMProgram_OriginalPGMProgram_Title',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.oa.pgm.basic.CheckGrid',
			classConfig: {
				title: fieldLabel,
				defaultWhere: me.OriginalDefaultWhere
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('PGMProgram_OriginalPGMProgram_Title');
					var Id = me.getComponent('PGMProgram_OriginalPGMProgram_Id');
					CName.setValue(record ? record.get('PGMProgram_Title') : '');
					Id.setValue(record ? record.get('PGMProgram_Id') : '');
					me.changeNewFileNameValue();
					p.close();
				}
			}
		};
	},
	/*
	 * 自动修改程序名称值
	 * 父节点-子节点-仪器分类-厂商(仪器品牌)-仪器型号-程序名(标题)-版本号
	 * 厂商-型号为在选择仪器后才会有
	 */
	changeNewFileNameValue: function() {
		var me = this;
		var Title = me.getComponent("PGMProgram_Title");

		var PBDictTreeCName = me.getComponent("PGMProgram_PBDictTree_CName");
		var SubBDictTreeCName = me.getComponent("PGMProgram_SubBDictTree_CName");

		var EquipType = me.getComponent("PGMProgram_EquipType_CName");
		var EquipFactoryBrand = me.getComponent("PGMProgram_BEquip_EquipFactoryBrand_CName");
		var Equipversion = me.getComponent("PGMProgram_BEquip_Equipversion");

		var VersionNo = me.getComponent("PGMProgram_VersionNo");

		var tempValue = PBDictTreeCName.getValue() + "-" + SubBDictTreeCName.getValue();

		if(EquipType && EquipType.getValue() != "") {
			tempValue = tempValue + "-" + EquipType.getValue();
		}
		if(EquipFactoryBrand && EquipFactoryBrand.getValue() != "") {
			tempValue = tempValue + "-" + EquipFactoryBrand.getValue();
		}
		if(Equipversion && Equipversion.getValue() != "") {
			tempValue = tempValue + "-" + Equipversion.getValue();
		}
		if(Title && Title.getValue() != "") {
			tempValue = tempValue + "-" + Title.getValue();
		}
		if(VersionNo && VersionNo.getValue() != "") {
			tempValue = tempValue + "-" + VersionNo.getValue();
		}
		var NewFileName = me.getComponent("PGMProgram_NewFileName");
		NewFileName.setValue(tempValue);
	},
	/***标题*/
	createPGMProgram_Title: function(fieldLabel) {
		var me = this;
		me.PGMProgram_Title = {
			fieldLabel: fieldLabel,
			itemId: 'PGMProgram_Title',
			name: 'PGMProgram_Title',
			allowBlank: false,
			emptyText: me.titleEmptyText,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeNewFileNameValue();
				}
			}
		};
	},
	/***文档编号*/
	createPGMProgram_No: function(fieldLabel) {
		var me = this;
		me.PGMProgram_No = {
			fieldLabel: fieldLabel,
			itemId: 'PGMProgram_No',
			name: 'PGMProgram_No'
		};
	},
	/***版本号*/
	createPGMProgram_VersionNo: function(fieldLabel) {
		var me = this;
		me.PGMProgram_VersionNo = {
			fieldLabel: fieldLabel,
			itemId: 'PGMProgram_VersionNo',
			name: 'PGMProgram_VersionNo',
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeNewFileNameValue();
				}
			}
		};
	},
	/***授权号*/
	createPGMProgram_SQH: function(fieldLabel) {
		var me = this;
		me.PGMProgram_SQH = {
			fieldLabel: fieldLabel,
			itemId: 'PGMProgram_SQH',
			name: 'PGMProgram_SQH'
		};
	},
	/*程序附件**/
	createPGMProgram_File: function(fieldLabel) {
		var me = this;
		me.PGMProgram_File = {
			fieldLabel: fieldLabel,
			name: 'file',
			itemId: 'PGMProgram_File',
			xtype: 'filefield',
			buttonText: '选择'
		};
	},
	/*原附件信息**/
	createPGMProgram_FileName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_FileName = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'PGMProgram_FileName',
			itemId: 'PGMProgram_FileName'
		};
	},
	/**关键字*/
	createPGMProgram_Keyword: function(fieldLabel) {
		var me = this;
		me.PGMProgram_Keyword = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_Keyword',
			itemId: 'PGMProgram_Keyword',
			emptyText: '逗号分割'
		};
	},
	/**摘要*/
	createPGMProgram_Memo: function(fieldLabel) {
		var me = this;
		me.PGMProgram_Memo = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_Memo',
			minHeight: 220,
			height: 235,
			//maxLength: 500,
			maxLengthText: "程序摘要说明信息",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		};
		if(me.formtype == "add") {
			me.PGMProgram_Memo.height = 250;
		}
	},
	/**创建发布日期*/
	createPGMProgram_PublisherDateTime: function(fieldLabel) {
		var me = this;
		me.PGMProgram_PublisherDateTime = {
			fieldLabel: fieldLabel,
			name: 'PGMProgram_PublisherDateTime',
			itemId: 'PGMProgram_PublisherDateTime',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
	},
	/*发布后是否可评论**/
	createPGMProgram_IsDiscuss: function(fieldLabel) {
		var me = this;
		me.PGMProgram_IsDiscuss = {
			boxLabel: fieldLabel,
			name: 'PGMProgram_IsDiscuss',
			itemId: 'PGMProgram_IsDiscuss',
			xtype: 'checkbox',
			checked: true,
			style: {
				marginLeft: '20px'
			}
		};
	},
	createPGMProgram_SubBDictTree_CName: function(fieldLabel) {
		var me = this;
		me.PGMProgram_SubBDictTree_CName = {
			fieldLabel: '分类',
			locked: me.isLeaf, //树节点为叶子节点时需要锁定只读
			readonly: me.isLeaf,
			emptyText: '必填项',
			name: 'PGMProgram_SubBDictTree_CName',
			itemId: 'PGMProgram_SubBDictTree_CName',
			IsnotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			//hidden: true,
			multiSelect: false,
			value: me.SubBDictTreeCName,
			onTriggerClick: function() {
				if(me.isLeaf == false) {
					JShell.Win.open('Shell.class.sysbase.dicttree.CheckTree', {
						resizable: false,
						isShowchecked: false,
						rootVisible: false,
						IDS: me.SubBDictTreeId,
						treeShortcodeWhere: 'idListStr=' + me.SubBDictTreeId,
						listeners: {
							accept: function(p, record) {
								me.onParentModuleAccept(record);
								p.close();
							}
						}
					}).show();
				}else{
					JShell.Msg.alert("当前分类为在树字典里为子节点,已被锁定,不能修改!",null,2000);
				}
			}
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//程序名称
		me.PGMProgram_NewFileName.colspan = 4;
		me.PGMProgram_NewFileName.width = me.defaults.width * me.PGMProgram_NewFileName.colspan;
		items.push(me.PGMProgram_NewFileName);

		//文档标题
		me.PGMProgram_Title.colspan = 4;
		me.PGMProgram_Title.width = me.defaults.width * me.PGMProgram_Title.colspan;
		items.push(me.PGMProgram_Title);

		//文档编号
		me.PGMProgram_No.colspan = 1;
		me.PGMProgram_No.width = me.defaults.width * me.PGMProgram_No.colspan;
		items.push(me.PGMProgram_No);
		//版本号
		me.PGMProgram_VersionNo.colspan = 1;
		me.PGMProgram_VersionNo.width = me.defaults.width * me.PGMProgram_VersionNo.colspan;
		items.push(me.PGMProgram_VersionNo);

		me.PGMProgram_DispOrder.colspan = 1;
		me.PGMProgram_DispOrder.width = me.defaults.width * me.PGMProgram_DispOrder.colspan;
		items.push(me.PGMProgram_DispOrder);

		me.PGMProgram_IsDiscuss.colspan = 1;
		items.push(me.PGMProgram_IsDiscuss);
		//授权号
		items = me.getAddTableLayoutOfSQH(items);
		
		//关键字
		me.PGMProgram_Keyword.colspan = 4;
		me.PGMProgram_Keyword.width = me.defaults.width * me.PGMProgram_Keyword.colspan;
		items.push(me.PGMProgram_Keyword);

		//分类
		me.PGMProgram_SubBDictTree_CName.colspan = 1;
		me.PGMProgram_SubBDictTree_CName.width = me.defaults.width * me.PGMProgram_SubBDictTree_CName.colspan;
		items.push(me.PGMProgram_SubBDictTree_CName);

		//用户选择
		me.PGMProgram_ClientName.colspan = 1;
		me.PGMProgram_ClientName.width = me.defaults.width * me.PGMProgram_ClientName.colspan;
		items.push(me.PGMProgram_ClientName);
		//厂家选择
		me.PGMProgram_OtherFactoryName.colspan = 2;
		me.PGMProgram_OtherFactoryName.width = me.defaults.width * me.PGMProgram_OtherFactoryName.colspan;
		items.push(me.PGMProgram_OtherFactoryName);

		//附件
		items = me.getAddTableLayoutOfFileNameRow(items);

		//文档备注
		me.PGMProgram_Memo.colspan = 4;
		me.PGMProgram_Memo.width = me.defaults.width * me.PGMProgram_Memo.colspan;
		items.push(me.PGMProgram_Memo);
		return items;
	},
	/**@overwrite 授权号*/
	getAddTableLayoutOfSQH: function(items) {
		var me = this;
		return items;
	},
	/**@overwrite 获取列表布局组件附件行*/
	getAddTableLayoutOfFileNameRow: function(items) {
		var me = this;
		me.PGMProgram_File.colspan = 4;
		me.PGMProgram_File.width = me.defaults.width * me.PGMProgram_File.colspan;
		items.push(me.PGMProgram_File);

		switch(me.formtype) {
			case 'edit':
				me.PGMProgram_FileName.colspan = 4;
				me.PGMProgram_FileName.width = me.defaults.width * me.PGMProgram_FileName.colspan;
				items.push(me.PGMProgram_FileName);
				break;
			default:
				break;
		}

		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PGMProgram_PublisherDateTime = JShell.Date.getDate(data.PGMProgram_PublisherDateTime);
		var reg = new RegExp("<br />", "g");
		data.PGMProgram_Memo = data.PGMProgram_Memo.replace(reg, "\r\n");
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},

	/**选择所属字典树*/
	onParentModuleAccept: function(record) {
		var me = this,
			SubBDictTreeId = me.getComponent('PGMProgram_SubBDictTree_Id'),
			SubBDictTreeCName = me.getComponent('PGMProgram_SubBDictTree_CName'),
			DataTimeStamp = me.getComponent('PGMProgram_SubBDictTree_DataTimeStamp'),

			PBDictTreeId = me.getComponent('PGMProgram_PBDictTree_Id'),
			PBDictTreeCName = me.getComponent('PGMProgram_PBDictTree_CName');

		SubBDictTreeId.setValue(record.get('tid'));
		SubBDictTreeCName.setValue(record.get('text') || '');

		PBDictTreeId.setValue(record.raw.pid);
		PBDictTreeCName.setValue(record.parentNode.get("text"));
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'PGMProgram_Id'
		}, {
			fieldLabel: '仪器Id',
			hidden: true,
			itemId: 'PGMProgram_BEquip_Id',
			name: 'PGMProgram_BEquip_Id'
		}, {
			fieldLabel: '仪器分类',
			hidden: true,
			itemId: 'PGMProgram_EquipType_CName',
			name: 'PGMProgram_EquipType_CName'
		}, {
			fieldLabel: '仪器品牌',
			hidden: true,
			itemId: 'PGMProgram_BEquip_EquipFactoryBrand_CName',
			name: 'PGMProgram_BEquip_EquipFactoryBrand_CName'
		}, {
			fieldLabel: '仪器型号',
			hidden: true,
			itemId: 'PGMProgram_BEquip_Equipversion',
			name: 'PGMProgram_BEquip_Equipversion'
		}, {
			fieldLabel: '用户Id',
			hidden: true,
			itemId: 'PGMProgram_ClientID',
			name: 'PGMProgram_ClientID'
		}, {
			fieldLabel: '厂家Id',
			hidden: true,
			itemId: 'PGMProgram_OtherFactoryID',
			name: 'PGMProgram_OtherFactoryID'
		}, {
			fieldLabel: '程序Id',
			hidden: true,
			itemId: 'PGMProgram_OriginalPGMProgram_Id',
			name: 'PGMProgram_OriginalPGMProgram_Id'
		}, {
			fieldLabel: '所属字典树上级节点ID',
			hidden: true,
			value: me.PBDictTreeId,
			itemId: 'PGMProgram_PBDictTree_Id',
			name: 'PGMProgram_PBDictTree_Id'
		}, {
			fieldLabel: '所属字典树上级节点名称',
			hidden: true,
			value: me.PBDictTreeCName,
			name: 'PGMProgram_PBDictTree_CName',
			itemId: 'PGMProgram_PBDictTree_CName'
		}, {
			fieldLabel: '所属字典树ID',
			hidden: true,
			value: me.SubBDictTreeId,
			itemId: 'PGMProgram_SubBDictTree_Id',
			name: 'PGMProgram_SubBDictTree_Id'
		}, {
			fieldLabel: '所属字典树DataTimeStamp',
			hidden: true,
			name: 'PGMProgram_SubBDictTree_DataTimeStamp'
		});
		return items;
	}
});