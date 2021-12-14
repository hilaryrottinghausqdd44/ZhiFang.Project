/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.serialGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '申请列表',
    //获取数据服务路径
    selectUrl: '',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: true,
    //排序字段
    defaultOrderBy: [],
    //带分页栏
    hasPagingtoolbar: true,
    //是否启用序号列
    hasRownumberer: false,
	hasSave:true,
    //是否默认选中数据
    autoSelect: true,
	testItemDetails:'',
    /**序号列宽度*/
    //rowNumbererWidth: 35,
    //改变状态名称颜色
    initComponent: function () {
        var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		
        me.columns = [
            {
                text: '颜色名',
                dataIndex: 'ColorName',sort:false,
                width: 50,align: 'center',
				renderer:function (v, meta, record) {
					var ColorName =  record.get('ColorName');
					meta.style="background-color:#0066FF;color:#000000;";
					return ColorName;		
				}
            }, {
                text: '颜色',
                dataIndex: 'ColorValue',sort:false,
                width: 40,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					meta.style="background-color:"+ColorValue+";";
					return "";		
				}
            }, {
                text: '条码值',
                dataIndex: 'SerialNo',sort:false,
                width: 100,align: 'center', 
				editor:{  
					/* allowBlank:true,
					updateEl: true, 
					field: {
						xtype: 'textfield'
					} */
				} ,
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					var SerialNo =  record.get('SerialNo');
					meta.style="background-color:"+ColorValue+";";
					/* var input =
						"<input id='barcode_list_row_value_" + ColorValue + "' style='width:100%' value='" + (SerialNo || "") + "'/>";
					return input; */
					return v;		
				} 
            }/* , {
                text: '样本类型',
                dataIndex: 'SampleTypeName',sort:true,
                width: 80,align: 'center',
				editor: {
					xtype:'combo',
					displayFields:'CName',
					valueField:"SampleTypeID",
					store:sampleStore
				},
				renderer:function (v, meta, record) {
					sampleStore.toValue();
					console.log(record);
					console.log(JShell.JSON.decode( record.get("SampleTypeDetail")));
					this.editor.store.add(JShell.JSON.decode( record.get("SampleTypeDetail")));	
				}
            } */, {
                text: '样本类型',
                dataIndex: 'SampleType',sort:true,
                width: 80,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					//meta.style="background-color:"+ColorValue+";";
					var list = record.raw.SampleTypeDetail || [],
						len = list.length,
						arr = [];
					arr.push(
						"<select  id='barcode_list_row_type_" + ColorValue + "'>"
					);
					var selected = false;
					for (vari = 0; i < len; i++) {
						if (list[i].selected) {
							selected = true; break;
						}
					}
					if (len > 0 && !selected) {
						list[0].selected = true;
					}

					for (var i = 0; i < len; i++) {
						arr.push("<option value='" + list[i].SampleTypeID + "'" + (list[i].selected ? " selected='selected'" : "") + ">" + list[i].CName + "</option>");
					}

					arr.push("</select>");
					return arr.join("");
				}
            },{dataIndex:'SampleTypeDetail',hidden:true},{dataIndex:'ItemNo',hidden:true}];
        me.callParent(arguments);
    },
	
	onSaveClick: function() {
		var me = this;
		var value  = me.getBarCodeList();
		if(!value){
			JShell.Msg.error("条码号不可为空！");
			return;
		}
		me.fireEvent("DataSave",value);		
	},		
	//获取条码列表
	getBarCodeList:function(){
		var me = this;
		var rows = me.store.getRange() || [],
			len = rows.length,			
			barCodeList = [];	
		//样本类型的值处理
		for (var i = 0; i < len; i++) {					
			var tColor = document.getElementById("barcode_list_row_type_"+rows[i].data.ColorValue),
			    index=tColor.selectedIndex ;
			rows[i].data.SampleType = tColor.options[index].value;
		}
	
		for (var i = 0; i < len; i++) {
			if (!rows[i].data.SerialNo) return null;
			if (!rows[i].raw.SampleTypeDetail || rows[i].raw.SampleTypeDetail.length == 0) return null;
			barCodeList.push({
				ColorValue: rows[i].data.ColorValue, //颜色值
				ColorName: rows[i].data.ColorName, //颜色名称
				BarCode: rows[i].data.SerialNo, //条码值
				SampleType: rows[i].data.SampleType || rows[i].raw.SampleTypeDetail[0].SampleTypeID, //样本类型
				ItemList: rows[i].data.ItemNo.split(',')//项目列表(id字符串数组)
			});
		}
	
		return barCodeList;
	},
	storeDataDispose:function(){
		var me = this;
		var newArr= [];
		var arrId = [];
		for(var item of me.testItemDetails){
		    if(arrId.indexOf(item['ColorValue']) == -1){
		        arrId.push(item['ColorValue']);
		        newArr.push(item);
		    }else{
				for(var arr of newArr){
					if(arr['ColorValue'] == item['ColorValue']){
						arr['ItemNo'] = arr['ItemNo']+"," + item['ItemNo'];
					}
				}
			}
		}
		
		me.store.removeAll();
		me.store.add(newArr);
		me.enableControl();
	}
	
});