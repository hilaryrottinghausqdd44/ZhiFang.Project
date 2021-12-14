/**
 * 经销商阶梯价格列表
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.contractprice.StepPriceGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商阶梯价格列表',

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDContractPrice',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_BeginDate',
		direction: 'DESC'
	}, {
		property: 'DContractPrice_SampleCount',
		direction: 'DESC'
	}],
	titleForm: '送检单位阶梯价格表单',
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
	/**合同价格类型（0:经销商,1:送检单位）*/
	ContractPriceType: '0',
	/**经销商ID*/
	DealerId: null,
	/**经销商时间戳*/
	DealerDataTimeStamp: null,

	/**送检单位ID*/
	BLaboratoryId: null,
	/**送检单位时间戳*/
	BLaboratoryDataTimeStamp: null,

	/**开始日期*/
	BeginDate: '',
	/**结束日期*/
	EndDate: '',
	/**合同编号*/
	ContractNo: '',
	/**项目ID*/
	ItemId: null,
	/**项目时间戳*/
	ItemDataTimeStamp: null,
	LaboratoryId: '',
//	IsStepPrice: '0',
	//PKField:'DContractPrice_Id',
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
		me.getView().on("refresh", function() {
			me.mergeCells(me, [2])
		});
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏'copy',
		me.buttonToolbarItems = ['refresh', 'edit','save'];
		//数据列
		me.columns = [{
			dataIndex: 'DContractPrice_BTestItem_CName',
			text: '项目名称',
			disabled: false,
			width: 120,
			defaultRenderer: true,
			sortable: true
		}, {
			dataIndex: 'DContractPrice_BeginDate',
			text: '开始日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_EndDate',
			text: '截止日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_SampleCount',
			text: '<b style="color:blue;">数量要求</b>',
			width: 80,
			hidden: true,
			editor: {
				xtype: 'numberfield',
				listeners: {
					blur: function(com, event, op) {
						JShell.Action.delay(function() {
							me.getView().refresh();
						}, null, 10);
					},
					focus: function(com, event, op) {
						var record = com.ownerCt.editingPlugin.context.record;
						if (record && (record.get("DContractPrice_IsStepPrice") == false || record.get("DContractPrice_IsStepPrice") == '否')) {
							com.setReadOnly(true);
						} else {
							com.setReadOnly(false);
						}
					}
				}
			},
			sortable: false
		}, {
			dataIndex: 'DContractPrice_StepPrice',
			text: '<b style="color:blue;">价格</b>',
			width: 80,
			editor: {
				xtype: 'numberfield'
			},
			sortable: false,
			renderer: function(value, meta) {
				var v = value == null ? '' : value;
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				if (!value || value == 0 || value == '0') {
					meta.style = 'background-color:red;';
				}
				return v;
			}
		}, {
			dataIndex: 'DContractPrice_IsStepPrice',
			//text: '<b style="color:blue;">是否阶梯价</b>', //格类型
			text: '是否阶梯价',
			width: 70,
			hidden: true,
			align: 'center',
			renderer: function(value, meta) {
				var v = value;
				var bgcolor = '';
				switch (v.toString().toLowerCase()) {
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
			},
			listeners: {
				change: function(com, newValue, oldValue, op) {
					JShell.Action.delay(function() {
						me.getView().refresh();
					}, null, 5);
				}
			}
		}, {
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同号',
			width: 100,
			hidden: true,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DContractPrice_BTestItem_Id',
			text: '项目ID',
			hidden: true,
			hideable: false
//			isKey: true
		}, {
			dataIndex: 'DContractPrice_BTestItem_DataTimeStamp',
			text: '项目时间戳',
			hidden: true,
			hideable: false
//			isKey: true
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
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		//me.openAddContractPriceWin();
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		me.openStepPriceForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get("DContractPrice_Id");
		me.openStepPriceForm(id);
	},
	/**打开原有的经销商阶梯价格表单*/
	openStepPriceForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			title: me.titleForm,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.ItemId = records[0].get("DContractPrice_BTestItem_Id");

		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.ContractNo = me.ContractNo;
			if (me.BeginDate) {
				var dt = new Date(me.BeginDate.toString());
				config.BeginDate = Ext.Date.format(dt, "Y-m-d");
			}
			if (me.EndDate) {
				var dt = new Date(me.EndDate.toString());
				config.EndDate = Ext.Date.format(dt, "Y-m-d");
			}
			config.ContractNoIsDisabled = true;
			if (me.BLaboratoryId != null) {
				config.BLaboratoryId = me.BLaboratoryId; //送检单位ID
				config.BLaboratoryDataTimeStamp = me.BLaboratoryDataTimeStamp; //送检单位时间戳
			} else {
				config.DealerId = me.DealerId; //经销商ID
				config.DealerDataTimeStamp = me.DealerDataTimeStamp; //经销商时间戳
			}
			config.ItemId = me.ItemId; //项目ID
			config.ItemDataTimeStamp = records[0].get("DContractPrice_BTestItem_DataTimeStamp"); //项目时间戳
		}

		JShell.Win.open('Shell.class.pki.contractprice.DunitContractPriceForm', config).show();

	},

	/**点击复制按钮处理*/
	onCopyClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Win.open('Shell.class.pki.stepprice.FormCopy', {
			formtype: 'add',
			ContractNo: records[0].get('DContractPrice_ContractNo'),
			listeners: {
				save: function(win, data) {
					me.saveCopyIfo(win, data)
				}
			}
		}).show();
	},
	/**保存拷贝信息*/
	saveCopyIfo: function(win, data) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		win.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			//			var count = rec.get('DContractPrice_SampleCount')
			var price = rec.get('DContractPrice_StepPrice');
			me.saveOneByEntity(win, rec.get("DContractPrice_Id"), {
				BeginDate: data.BeginDate,
				EndDate: data.EndDate,
				ContractNo: data.ContractNo,

				StepPrice: price,
				//				SampleCount: count,

				StepPriceMemo: '价格=' + price + '元',

				BDealer: {
					Id: me.DealerId,
					DataTimeStamp: me.DealerDataTimeStamp.split(',')
				},
				BTestItem: {
					Id: me.ItemId,
					DataTimeStamp: me.ItemDataTimeStamp.split(',')
				}
			});
		}
	},
	/**保存一条复制的数据*/
	saveOneByEntity: function(win, id, entity) {
		var me = this;
		var url = (win.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + win.addUrl;

		var params = Ext.JSON.encode({
			entity: entity
		});
		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord("DContractPrice_Id", id);
			if (data.success) {
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				win.hideMask(); //隐藏遮罩层
				win.close();
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;
		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get("DContractPrice_Id");
			//			var count = rec.get('DContractPrice_SampleCount');
			var price = rec.get('DContractPrice_StepPrice');
			me.updateOneBySampleCountAndPrice(id, price);
		}
	},
	/**修改数量和价格*/
	updateOneBySampleCountAndPrice: function(id, price) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				//				SampleCount: count,
				StepPrice: price,
//				IsStepPrice: me.IsStepPrice,
				StepPriceMemo: '价格=' + price + '元'
			},
			fields: 'Id,StepPrice,StepPriceMemo' //StepPriceMemo,ConfirmUser,ConfirmTime'
		});
		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord("DContractPrice_Id", id);
			if (data.success) {
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**根据经销商ID和项目ID获取数据*/
	loadByDealerIdAndItemId: function(dealerId, itemId) {
		var me = this;
		me.DealerId = dealerId;
		me.ItemId = itemId;
		me.defaultWhere = 'dcontractprice.BDealer.Id=' + dealerId +
			' and dcontractprice.BTestItem.Id=' + itemId;
		me.onSearch();
	},
	/**根据合同编号获取数据*/
	loadByContractNo: function(contractNo, dealerId) {
		var me = this;
		me.DealerId = dealerId;
		//me.ItemId = itemId;
		me.defaultWhere = "dcontractprice.BDealer.Id='" + dealerId +
			"' and dcontractprice.ContractNo='" + contractNo + "'";
		me.onSearch();
	},
	/**根据合同编号获取送检单位合同数据*/
	loadByBLaboratoryContractNo: function(contractNo, blaboratoryId) {
		var me = this;
		me.BLaboratoryId = blaboratoryId;
		//me.ItemId = itemId;
		me.defaultWhere = "dcontractprice.BLaboratory.Id='" + blaboratoryId +
			"' and dcontractprice.ContractNo='" + contractNo + "'";
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
			        
			if (rowspanObj.rowspan  !=  1) {            
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
			        
			for (var  i = 1; i < trCount; i++) { //i=0表示表头等没用的行
				var record = grid.store.getAt(i - 1); //===========
				            
				arrayTd  =  arrayTr[i].getElementsByTagName("td");            
				var  cold = 0;

				            
				col = colIndex + cold; //跳过RowNumber列和check列
				            
				if (!divHtml) {
					checkV = record.get(grid.checkField); //===========
					                
					divHtml  =  arrayTd[col - 1].innerHTML;                
					rowspanObj  =   {
						tr: i,
						td: col,
						rowspan: rowspan
					}            
				} else {
					var nowV = record.get(grid.checkField);            
					var  cellText  =  arrayTd[col - 1].innerHTML;                
					var  addf = function() {                    
						rowspanObj["rowspan"]  =  rowspanObj["rowspan"] + 1;                    
						removeObjs.push({
							tr: i,
							td: col
						});                    
						if (i == trCount - 1)                         merge(rowspanObj, removeObjs); //执行合并函数
						                
					};                
					var  mergef = function() {                    
						merge(rowspanObj, removeObjs);
						checkV = nowV;     
						divHtml  =  cellText;                    
						rowspanObj  =   {
							tr: i,
							td: col,
							rowspan: rowspan
						}                    
						removeObjs  =   [];                
					};                
					if (cellText  ==  divHtml) {
						if (isCheckColumn) { //勾选列
							if (nowV == checkV) {
								record.set(grid.displayField, true);
								addf();
							} else {
								record.set(grid.displayField, false);
								mergef();
							}
						} else {
							if (colIndex != cols[0]) {                        
								var  leftDisplay = arrayTd[col - 2].style.display; //判断左边单元格值是否已display
								                        
								if (leftDisplay == 'none')                             addf();                        
								else                             mergef();                    
							} else                         addf();
						}                
					} else                     mergef();            
				}        
			}    
		});
	}
});