/**
 * 合同价格列表
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealercontract.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '合同价格列表',

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/StatService.svc/Stat_UDTO_AddDContractPrice',
	/**修改服务地址*/
	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDContractPrice',
	/**默认加载*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_EndDate',
		direction: 'ASC'
	}],
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	BDealerId: null,
	BDealerDataTimeStamp: null,
	BDealerCName: "",
	/**送检单位ID*/
	LaboratoryId: null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp: null,

	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId: null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName: null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp: null,

	/**是否带功能按钮*/
	hasButtons: true,
	/**是否带修改价格功能*/
	canEditPrice: true,
	/*新增合同打开的UI*/
	AddType: "AddContractPrice",
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//编辑功能(可以修改经销商或送检单位的合同截止日期及合同编号)
		if(me.hasButtons) {
			me.on({
				itemdblclick: function(view, record) {
					me.onEditClick();
				}
			});
		}

		me.getView().on("refresh", function() {
			me.mergeCells(me, [2])
		});
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '合同编号',
			isLike: true,
			fields: ['dcontractprice.ContractNo']
		};

		if(me.hasButtons) {
			me.buttonToolbarItems.push({
				text: '新增合同',
				iconCls: 'button-add',
				tooltip: '<b>新增合同</b>',
				handler: function() {
					switch(me.AddType) {
						//经销商合同
						case "AddContractPrice":
							me.openAddContractPriceWin();
							break;
							//送检单位合同
						case "AddDunitContractPrice":
							me.AddDunitContractPrice();
							break;
						default:
							break;
					}

				}
			},'edit',{
				text: '修改合同时间',
				iconCls: 'button-edit',
				tooltip: '<b>修改合同时间</b>',
				handler: function() {
				    me.onEditContractDateClick();
				}
			}, 'del'); //'add',
		}
		if(me.canEditPrice) {
			me.buttonToolbarItems.push('save');
		}
		me.buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});

		me.columns = [];

		me.columns.push({
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同号',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_BeginDate',
			text: '合同起始日',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_EndDate',
			text: '合同终止日',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_BTestItem_CName',
			text: '项目',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_SampleCount',
			text: '数量',
			width: 45,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_StepPrice',
			text: '价格',
			width: 45,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_IsStepPrice',
			text: '<b style="color:blue;">是否阶梯价</b>', //格类型
			width: 85,
			//hidden:true,
			//isBool: true,
			align: 'center',
			//type: 'bool',
			//			editor: {
			//				xtype: 'uxBoolComboBox'
			//			},
			renderer: function(value, meta) {
				var v = value;
				var bgcolor = '';
				switch(v.toString().toLowerCase()) {
					case "true":
						bgcolor = 'green';
						v = "是";
						break;
					case "1":
						bgcolor = 'green';
						v = "是";
						break;
					default:
						bgcolor = 'red';
						v = "否";
						break;
				}
				meta.style = 'color:balck;background-color:' + bgcolor || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPrice_AddUser',
			text: '合同录入人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_AddTime',
			text: '合同录入时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DContractPrice_ConfirmUser',
			text: '合同确认人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ConfirmTime',
			text: '合同确认时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		});

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		//me.openContractPriceForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openContractPriceForm(id);
	},
	/**@overwrite 修改合同时间点击处理方法*/
	onEditContractDateClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openContractDateForm(id);
	},
	/**打开表单*/
	openContractPriceForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			BDealerId: me.BDealerId,
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp, //送检单位默认开票方时间戳
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		switch(me.AddType) {
			//经销商合同
			case "AddContractPrice":
				config.hiddenIsStepPrice = false;
				break;
			case "AddDunitContractPrice":
				config.hiddenIsStepPrice = true;
				break;
			default:
				config.hiddenIsStepPrice = true;
				break;
		}
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
			JShell.Win.open('Shell.class.pki.dealer.contractprice.DContractPriceEditForm', config).show();
		} else {
			JShell.Win.open('Shell.class.pki.contractprice.Form', config).show();
		}

	},
	/**打开表单*/
	openContractDateForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'edit',
			BDealerId: me.BDealerId,
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp, //送检单位默认开票方时间戳
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		config.title = '修改合同时间';
		config.hiddenIsStepPrice = true;
		config.formtype = 'edit';
		config.PK = id;
		config.height = 165;
		config.IsContractNoEnable=true;
		config.editUrl='/StatService.svc/Stat_UDTO_UpdateDContractDateByField';
		JShell.Win.open('Shell.class.pki.dealer.contractprice.DContractPriceEditForm', config).show();

	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var price = rec.get('DContractPrice_ContractPrice');
			me.updateOneByPrice(id, price);
		}
	},
	/**修改价格*/
	updateOneByPrice: function(id, price) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				ContractPrice: price
			},
			fields: 'Id,ContractPrice,ConfirmUser,ConfirmTime'
		});
		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord(me.PKField, id);
			if(data.success) {
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**新增合同价格*/
	openAddContractPriceWin: function() {
		var me = this;
		if(me.BDealerId=='0' || !me.BDealerId){
			JShell.Msg.error('请选择具体的经销商');
			return;
		}
		JShell.Win.open('Shell.class.pki.dealercontract.AddApp1', {
			resizable: false,
			BDealerId: me.BDealerId,
			BDealerCName: me.BDealerCName,
			BDealerDataTimeStamp: me.BDealerDataTimeStamp,
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp, //送检单位默认开票方时间戳
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		}).show();
	},
	/**复制合同价格*/
	openCopyContractPriceWin: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Win.open('Shell.class.pki.dealercontract.FormCopy', {
			resizable: false,
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp, //送检单位默认开票方时间戳
			listeners: {
				save: function(p, entity) {
					me.onSaveCopyInfo(p, entity);
				}
			}
		}).show();
	},
	/**保存信息*/
	onSaveCopyInfo: function(p, data) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];

		me.showMask(me.saveText); //显示遮罩层
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var entity = me.getEntity(rec, data);
			me.saveOne(i, p, entity);
		}
	},
	/**获取数据对象*/
	getEntity: function(record, data) {
		var me = this;
		var entity = {
			BeginDate: JShell.Date.toServerDate(record.get('DContractPrice_BeginDate')),
			EndDate: JShell.Date.toServerDate(record.get('DContractPrice_EndDate')),
			ContractNo: record.get('DContractPrice_ContractNo'),
			ContractPrice: record.get('DContractPrice_ContractPrice'),
			BLaboratory: {
				Id: me.LaboratoryId,
				DataTimeStamp: me.LaboratoryDataTimeStamp.split(',')
			},
			BDealer: {
				Id: record.get('DContractPrice_BDealer_Id'),
				DataTimeStamp: record.get('DContractPrice_BDealer_DataTimeStamp').split(',')
			},
			BTestItem: {
				Id: record.get('DContractPrice_BTestItem_Id'),
				DataTimeStamp: record.get('DContractPrice_BTestItem_DataTimeStamp').split(',')
			},
			BBillingUnit: {
				Id: record.get('DContractPrice_BBillingUnit_Id'),
				DataTimeStamp: record.get('DContractPrice_BBillingUnit_DataTimeStamp').split(',')
			}
		};
		//开票类型
		if(record.get('DContractPrice_BillingUnitType')) {
			entity.BillingUnitType = record.get('DContractPrice_BillingUnitType');
		}
		//科室
		if(record.get('DContractPrice_BDept_Id')) {
			entity.BDept = {
				Id: record.get('DContractPrice_BDept_Id'),
				DataTimeStamp: record.get('DContractPrice_BDept_DataTimeStamp').split(',')
			};
		}

		for(var i in data) {
			entity[i] = data[i];
		}

		return entity;
	},
	/**保存一条数据*/
	saveOne: function(index, p, entity) {
		var me = this;
		var url = (me.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;

		var params = Ext.JSON.encode({
			entity: entity
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				me.saveIndex++;
				if(!data.success) {
					me.saveError.push(data.msg);
				}
				if(me.saveIndex == me.saveCount) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveError.length == 0) {
						JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
						me.onSearch();
						p.close();
					} else {
						JShell.Msg.error(me.saveError.join('</br>'));
					}
				}
			});
		}, 100 * index);
	},
	/**根据经销商ID获取数据*/
	loadByBDealerId: function(id) {
		var me = this;
		me.defaultWhere = 'dcontractprice.BDealer.Id=' + id;
		me.onSearch();
	},
	/**新增送检单合同价格*/
	AddDunitContractPrice: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.contractprice.AddApp1', {
			resizable: false,
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp, //送检单位默认开票方时间戳
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		}).show();
	},
	/**根据送检单位ID获取数据*/
	loadByLaboratoryId: function(id) {
		var me = this;
		me.defaultWhere = 'dcontractprice.BLaboratory.Id=' + id;
		me.onSearch();
	},
	/**
	 * Ext.grid.Panel合并单元格
	 * @param {} grid  要合并单元格的grid对象
	 * @param {} cols  要合并哪几列 [1,2,4]
	 */
	mergeCells: function(grid, cols) {    
		var  arrayTr = document.getElementById(grid.getId() + "-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');    
		var  trCount  =  arrayTr.length;    
		var  arrayTd;    
		var  td;    
		var  merge  =   function(rowspanObj, removeObjs) { //定义合并函数
			        
			if(rowspanObj.rowspan  !=  1) {            
				arrayTd  = arrayTr[rowspanObj.tr].getElementsByTagName("td"); //合并行
				            
				td = arrayTd[rowspanObj.td - 1];            
				td.rowSpan = rowspanObj.rowspan;            
				td.vAlign = "middle";                            
				Ext.each(removeObjs, function(obj) {  //隐身被合并的单元格    
					                
					arrayTd  = arrayTr[obj.tr].getElementsByTagName("td");                   
					arrayTd[obj.td - 1].style.display = 'none';                           
				});           
			}          
		};         
		var  rowspanObj  =   {};  //要进行跨列操作的td对象{tr:1,td:2,rowspan:5}        
		    
		var  removeObjs  =   [];  //要进行删除的td对象[{tr:2,td:2},{tr:3,td:2}]    
		    
		var  col;       
		Ext.each(cols, function(colIndex) {  //逐列去操作tr
			var isCheckColumn = grid.columns[colIndex - 1].xtype == 'checkcolumn'; //是否是勾选列
			        
			var  rowspan  =  1;        
			var  divHtml  =  null; //单元格内的数值
			var checkV = null; //勾选框的值===========
			        
			for(var  i = 1; i < trCount; i++) { //i=0表示表头等没用的行
				var record = grid.store.getAt(i - 1); //===========
				            
				arrayTd  =  arrayTr[i].getElementsByTagName("td");            
				var  cold = 0;
				//          Ext.each(arrayTd,function(Td){ //获取RowNumber列和check列
				//              if(Td.getAttribute("class").indexOf("x-grid-cell-special") != -1)
				//                  cold++;
				//          });
				            
				col = colIndex + cold; //跳过RowNumber列和check列
				            
				if(!divHtml) {
					checkV = record.get(grid.checkField); //===========
					                
					divHtml  =  arrayTd[col - 1].innerHTML;                
					rowspanObj  =   {
						tr: i,
						td: col,
						rowspan: rowspan
					}            
				} else {
					var nowV = record.get(grid.checkField); //===========
					                
					var  cellText  =  arrayTd[col - 1].innerHTML;                
					var  addf = function() {                    
						rowspanObj["rowspan"]  =  rowspanObj["rowspan"] + 1;                    
						removeObjs.push({
							tr: i,
							td: col
						});                    
						if(i == trCount - 1)                         merge(rowspanObj, removeObjs); //执行合并函数
						                
					};                
					var  mergef = function() {                    
						merge(rowspanObj, removeObjs); //执行合并函数
						checkV = nowV; //==============
						                    
						divHtml  =  cellText;                    
						rowspanObj  =   {
							tr: i,
							td: col,
							rowspan: rowspan
						}                    
						removeObjs  =   [];                
					};                
					if(cellText  ==  divHtml) {
						if(isCheckColumn) { //勾选列
							if(nowV == checkV) {
								record.set(grid.displayField, true);
								addf();
							} else {
								record.set(grid.displayField, false);
								mergef();
							}
						} else {
							if(colIndex != cols[0]) {                        
								var  leftDisplay = arrayTd[col - 2].style.display; //判断左边单元格值是否已display
								                        
								if(leftDisplay == 'none')                             addf();                        
								else                             mergef();                    
							} else                         addf();
						}                
					} else                     mergef();            
				}        
			}    
		});
	}
});