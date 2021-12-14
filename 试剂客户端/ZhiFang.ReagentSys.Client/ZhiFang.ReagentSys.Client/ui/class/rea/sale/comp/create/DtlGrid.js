/**
 * 供货明细列表-供应商
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.comp.create.DtlGrid', {
	extend: 'Shell.class.rea.sale.basic.DtlGrid',
	title: '供货明细列表-供应商',
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
    rowIdx: null,
    colIdx: null,
    isSpecialkey: false,
    specialkeyArr: [{
        key: Ext.EventObject.ENTER,
        type: "down"
    }],
    /**是否已拆分*/
	IsSplit: 0,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    me.createCellEditListeners();
	  
	    var edit = me.getPlugin('NewsGridEditing');  
        me.on({
            validateedit: function(editor, e) {
                var bo = false;
                bo = me.fireEvent("cellAvailable", editor, e) === false ? false: true;
                if (me.rowIdx != null && me.colIdx != null) {
                    if (me.isSpecialkey) {
                    	var edit = me.getPlugin('NewsGridEditing');  
                        edit.startEditByPosition({
                            row: me.rowIdx,
                            column: me.colIdx
                        })
                    }
                    me.rowIdx = null;
                    me.colIdx = null;
                    me.isSpecialkey = false
                }
                return bo
            },
            beforeedit : function(editor, e) {
				if(!me.canEdit) return false;
				var isEdit=true;
				if(me.IsSplit=='1'){
					if(e.column.dataIndex=='BmsCenSaleDtl_MixSerial' || e.column.dataIndex=='BmsCenSaleDtl_RegisterNo' || e.column.dataIndex=='BmsCenSaleDtl_RegisterInvalidDate' ){
						isEdit=true;
					}else{
						isEdit=false;
					}
					return isEdit;
//					return e.colIdx == 4 ? true : false;
		    	}else{ 
		    	    return e.colIdx == 4 ? false : true;
		    	}
		    	
			}
        });
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_Goods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			renderer:function(value, meta) {
				var v = "";
				if(value == "0"){
					v = "批条码";
					meta.style = "color:green;";
				}else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				}else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ShortCode',
			text: '产品简码',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',
			text: '厂商产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_MixSerial',sortable: false,
			width: 200,
			text:'<b style="color:orange;">混合条码</b>',
			editor:{selectOnFocus:true}
		},{
			dataIndex:'BmsCenSaleDtl_LotNo',sortable: false,
			text:'<b style="color:blue;">产品批号</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_InvalidDate',sortable: false,
			text:'<b style="color:blue;">有效期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		},{
			dataIndex:'BmsCenSaleDtl_GoodsQty',sortable: false,
			text:'<b style="color:blue;">数量</b>',
			width:80,type:'int',align:'right',
			editor:{xtype:'numberfield',minValue:0,allowBlank:false}
		},{
			dataIndex:'BmsCenSaleDtl_AcceptCount',sortable: false,
			text:'验收数量',width:80,type:'int',align:'right',hidden:true
		},{
			dataIndex: 'BmsCenSaleDtl_BiddingNo',sortable: false,
		    text:'<b style="color:blue;">招标号</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_TempRange',sortable: false,
		    text:'<b style="color:blue;">温度范围</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_ProdDate',sortable: false,
		    text:'<b style="color:blue;">生产日期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		},{
			dataIndex: 'BmsCenSaleDtl_ProdOrgName',sortable: false,
		    text:'<b style="color:blue;">生产厂家</b>',
			width:100,editor:{}
		},{
			dataIndex:'BmsCenSaleDtl_Price',sortable: false,
			text:'<b style="color:blue;">单价</b>',
			width:80,type:'float',align:'right',
			editor:{xtype:'numberfield',minValue:0,decimalPrecision:3,allowBlank:false}
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_RegisterNo',sortable: false,
			text:'<b style="color:green;">注册证编号</b>',
			width:100,align:'right',
			editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_RegisterInvalidDate',sortable: false,
			text:'<b style="color:green;">注册证有效期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_TaxRate',sortable: false,
			text: '税率',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdDate',sortable: false,
			text: '生产日期',
			align:'center',
			width: 90,
			type:'date',
			isDate: true
		},{
			dataIndex: 'BmsCenSaleDtl_BiddingNo',sortable: false,
			text: '招标号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsSerial',sortable: false,
			text: '产品条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_LotSerial',sortable: false,
			text: '批号条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_PackSerial',sortable: false,
			text: '包装单位条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_DataAddTime',sortable: false,
			text: '新增时间',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_EName',sortable: false,
			text: '产品英文名',
			hidden: true,
			hideable: false
		}];
		
	    me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		return columns;
	},
	/**新增勾选*/
	onAddClick:function(){
		var me = this;
		if(!me.DocInfo.CenOrgId){
			JShell.Msg.warning('CenOrgId参数不存在!');
		}
		var defaultWhere = 'goods.CenOrgConfirm=1 and goods.CompConfirm=1 and goods.Comp.Id=' + 
			me.DocInfo.CompId + 'and goods.CenOrg.Id=' + me.DocInfo.CenOrgId;
			
		JShell.Win.open('Shell.class.rea.goods.CheckGrid',{
			defaultWhere:defaultWhere,
			listeners:{
				accept:function(p,records){
					me.onAccept(p,records);
				}
			}
		}).show();
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items;
			
		var isError = false;
		for(var i=0;i<records.length;i++){
			var rec = records[i];
			if(!rec.get('BmsCenSaleDtl_LotNo') || !rec.get('BmsCenSaleDtl_InvalidDate') || 
				(!rec.get('BmsCenSaleDtl_GoodsQty') && rec.get('BmsCenSaleDtl_GoodsQty') !== 0)){
				isError = true;
				break;
			}
		}
		if(isError){
			JShell.Msg.error('产品批号、有效期、数量都不能为空，请填写完整后保存！');
			return;
		}
		
		var changedRecords = me.store.getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
			
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			me.updateOne(i,changedRecords[i]);
		}
	},
	/**确定保存*/
	onAccept:function(p,records){
		var me = this,
			len = records.length,
			arr = [];
			
		for(var i=0;i<len;i++){
			var rec = records[i];
			arr.push({
				BmsCenSaleDoc:{Id:me.DocInfo.SaleDocID},
				SaleDtlNo:'1',
				SaleDocNo:me.DocInfo.SaleDocNo,
				Goods:{Id:rec.get('Goods_Id')},
				ProdGoodsNo:rec.get('Goods_ProdGoodsNo'),
				Prod:{Id:rec.get('Goods_Prod_Id')},
				ProdOrgName:rec.get('Goods_ProdOrgName'),
				GoodsName:rec.get('Goods_CName'),
				GoodsUnit:rec.get('Goods_UnitName'),
				UnitMemo:rec.get('Goods_UnitMemo'),
				GoodsQty:1,
				Price:rec.get('Goods_Price'),
				SumTotal:rec.get('Goods_Price'),
				TaxRate:rec.get('Goods_TaxRate') || 0,
				LotNo:rec.get('Goods_LotNo'),
				ProdDate:JShell.Date.toServerDate(rec.get('Goods_ProdDate')),
				InvalidDate:JShell.Date.toServerDate(rec.get('Goods_InvalidDate')),
				BiddingNo:rec.get('Goods_BiddingNo'),
				IOFlag:0,
				GoodsSerial:rec.get('Goods_GoodsSerial'),
				PackSerial:rec.get('Goods_PackSerial'),
				LotSerial:rec.get('Goods_LotSerial'),
				MixSerial:rec.get('Goods_MixSerial'),
				ShortCode:rec.get('Goods_ShortCode'),
				BarCodeMgr:rec.get('Goods_BarCodeMgr'),
				Visible:1
			});
		}
		me.saveLength = len;
		me.saveCount = 0;
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			me.addOne(i,Ext.JSON.encode({entity:arr[i]}));
		}
	},
	/**保存一条数据*/
	addOne:function(index,params){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				me.saveCount++;
				if(me.saveCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					me.onSearch();
				}
			});
		},100 * index);
	},
	/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('BmsCenSaleDtl_Id'),
				LotNo:record.get('BmsCenSaleDtl_LotNo'),
				InvalidDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_InvalidDate')),
				GoodsQty:record.get('BmsCenSaleDtl_GoodsQty'),
				AcceptCount:record.get('BmsCenSaleDtl_AcceptCount'),
				Price:record.get('BmsCenSaleDtl_Price'),
				SumTotal:record.get('BmsCenSaleDtl_SumTotal'),
				MixSerial:record.get('BmsCenSaleDtl_MixSerial'),
				RegisterNo:record.get('BmsCenSaleDtl_RegisterNo'),
				RegisterInvalidDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_RegisterInvalidDate')),
				BiddingNo:record.get('BmsCenSaleDtl_BiddingNo'),
				TempRange:record.get('BmsCenSaleDtl_TempRange'),
				ProdOrgName:record.get('BmsCenSaleDtl_ProdOrgName'),
				ProdDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_ProdDate'))
			},
			fields:'Id,LotNo,InvalidDate,GoodsQty,AcceptCount,Price,SumTotal,MixSerial,RegisterNo,RegisterInvalidDate,BiddingNo,TempRange,ProdDate,ProdOrgName'
		});
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
				if(record){
					record.set(me.DelField,true);
					record.commit();
				}
			}else{
				me.saveErrorCount++;
				if(record){
					record.set(me.DelField,false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		},false);
	},
	createCellEditListeners: function() {
        var me = this,
        columns = me.columns;
        for (var i in columns) {
            var column = columns[4];
            if (column.editor) {
                column.editor.listeners = column.editor.listeners || {};
                column.editor.listeners.specialkey = function(textField, e) {
                    me.doSpecialkey(textField, e)
                };
                column.hasEditor = true
            } else if (column.columns) {
                for (var j in column.columns) {
                    var c = column.columns[j];
                    if (c.editor) {
                        c.editor.listeners = c.editor.listeners || {};
                        c.editor.listeners.specialkey = function(textField, e) {
                            me.doSpecialkey(textField, e)
                        };
                        c.hasEditor = true
                    }
                }
            }
        }
    },
    doSpecialkey: function(textField, e) {
        var me = this;
        var info = me.getKeyInfo(e);
        if (info) {
            me.isSpecialkey = true;
            e.stopEvent();
            me.changeRowIdxAndCelIdx(textField, info.type);
            textField.blur()
        }
    },
    getKeyInfo: function(e) {
        var me = this,
        arr = me.specialkeyArr,
        key = e.getKey();
        var info = null;
        for (var i in arr) {
            var ctrlKey = arr[i].ctrlKey ? true: false;
            var shiftKey = arr[i].shiftKey ? true: false;
            if (arr[i].key == key && ctrlKey == e.ctrlKey && shiftKey == e.shiftKey) {
                if (arr[i].replaceKey) {
                    e.keyCode = arr[i].replaceKey;
                    info = null;
                    break
                } else {
                    info = arr[i];
                    break
                }
            }
        }
        return info
    },
    changeRowIdxAndCelIdx: function(field, type) {
        var me = this,
        context = field.ownerCt.editingPlugin.context,
        rowIdx = context.rowIdx,
        colIdx = context.colIdx;
        me.rowIdx = rowIdx;
        me.colIdx = colIdx;
        if(rowIdx+1== me.getStore().getCount()){
			JShell.Msg.alert("已经没有试剂了!");
			return;
		}
        if (type == "down") {
            me.rowIdx = me.getNextRowIndex(rowIdx, true)
        } 
    },
    getNextRowIndex: function(rowIdx, isDown) {
        var me = this,
        count = me.store.getCount(),
        nRowIdx = rowIdx;
        if (count == 0) return null;
        isDown ? nRowIdx++:nRowIdx--;
        if (nRowIdx == count) {
            nRowIdx = count - 1
        }
        if (nRowIdx == -1) {
            nRowIdx = 0
        }
        return nRowIdx
    },
	/**显示按钮*/
	showCheckButton:function(IsSplit){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			add = buttonsToolbar.getComponent('add'),
			del = buttonsToolbar.getComponent('del');
		var showEdit = true;
	    del.hide();
	    add.hide();
		if(IsSplit=='1'){
			showEdit=false;
		}
		if(showEdit){
	        del.show();
	        add.show();
		}
	}
});