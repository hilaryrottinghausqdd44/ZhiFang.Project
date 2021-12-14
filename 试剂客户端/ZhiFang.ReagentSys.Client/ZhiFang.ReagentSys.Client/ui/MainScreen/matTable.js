/**
	@name：质控物列表
	@author：liuyujie
	@version 2021-01-22
 */
layui.extend({
    uxtable: 'ux/table',
	soulTable:'ux/other/soulTable/soulTable'
}).define(['table', 'uxtable','soulTable'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        soulTable = layui.soulTable,
        uxtable = layui.uxtable;
		
	var tableObj = {
	    fields: {
	        IsUse: 'LBQCPMat_IsUse'
	    }
	};
	var href = window.document.location.href;
	var pathname = window.document.location.pathname;
	var pos = href.indexOf(pathname);
	var localhostPaht = href.substring(0, pos);
	var projectName = pathname.substring(0, pathname.substr(1).indexOf('/') + 1);
    //参数配置
    var config = {
        selectUrl:localhostPaht + projectName  + '/ReaManageService.svc/RS_UDTO_SearchReaGoodsQtyWarningInfo?isPlanish=true',
		time:3,
		isScroll:true,
        elem: '',
		cursor:1,
		GoodsClassType:null,
		limit:10
    };
    var matTable = {
        config: {
            PK: null,
            delIndex: null
        },
        //核心入口
        render: function (options,data) {
            var me = this;
            config.elem = options.id;
			//options.url=config.selectUrl+"&where=reagoods.GoodsClassType='"+options.GoodsClassType+"'";
            var table_options = {
                elem: options.elem,
                id: options.id,
                toolbar: '',
                page: false,
				limit: options.limit,
				limits: [20,40,60,80,100,200,500],
                autoSort: false, //禁用前端自动排序
                loading: true,
                size: 'sm', //小尺寸的表格
                height: options.height ? options.height : 'full-140',
				filter: {
					items:['column'],
					cache: true,
					bottom: false,
					manualOpen:true
				},
				data:data,
                cols: [[
					{ type: 'numbers', title: '行号' },
                    { field: 'ReaGoodsStockWarning_ReaCompanyID', width: 60, title: '主键ID',hide: true },
                    { field: 'ReaGoodsStockWarning_CompanyName', width: 100, title: '供应商' ,filter:true},
                    { field: 'ReaGoodsStockWarning_GoodsClass', width: 100, title: '一级分类' ,filter:true},
                    { field: 'ReaGoodsStockWarning_GoodsClassType', width: 100, title: '二级分类' ,filter:true},
                    { field: 'ReaGoodsStockWarning_GoodsCName', width: 100, title: '货品名称',filter:true},
					{ field: 'ReaGoodsStockWarning_ReaGoodsNo', width: 100, title: '货品编码',filter:true},
					{ field: 'ReaGoodsStockWarning_LotNo', width: 60, title: '货品批号',filter:true},
					{ field: 'ReaGoodsStockWarning_ProdOrgName', width: 100, title: '厂家' ,filter:true},
					{ field: 'ReaGoodsStockWarning_GoodsQty', width: 100, title: '库存数' ,filter:true},
					{ field: 'ReaGoodsStockWarning_UnitName', width: 100, title: '单位' ,filter:true},
					{ field: 'ReaGoodsStockWarning_UnitMemo', width: 100, title: '规格',filter:true},
					{ field: 'ReaGoodsStockWarning_Price', width: 60, title: '单价',filter:true},
					{ field: 'ReaGoodsStockWarning_SumTotal', width: 100, title: '总金额' ,filter:true},
					{ field: 'ReaGoodsStockWarning_DataAddTime', width: 100, title: '入库时间' ,filter:true},
					{ field: 'ReaGoodsStockWarning_InvalidDate', width: 100, title: '有效期' ,filter:true},
					{ field: 'ReaGoodsStockWarning_StockWarningByGoods', width: 100, title: '库存整体状态',filter:true},
					{ field: 'ReaGoodsStockWarning_StockWarning', width: 100, title: '库存状态',filter:true},
					{ field: 'ReaGoodsStockWarning_ValidDateWarning', width: 100, title: '效期状态',filter:true}
                ]],
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					
					//layui.event(options.id, "done", { res: res});
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                },
                done: function (res, curr, count) {
					layui.event(options.id, "done", { res: res});
					// 渲染之前先删除可能存在的表格列下来框
					if($('#soul-filter-list' + options.id).length != 0) {
						$('#soul-filter-list' + options.id).remove();
					}
					soulTable.render(this);
                    if (count == 0) $("#matTable+div .layui-table-main").html('<div class="layui-none">暂无相关数据</div>');
					
					if(config.isScroll&&config.isScroll!="false"){
						var firstRow = $("#matTablepanel").find('table').find('tbody').find('tr:first');
						var  rowHeight = firstRow.height()*(res.data.length+3);
						var mainheight = document.getElementsByClassName("layui-table-main")[0].offsetHeight;
						if(mainheight<rowHeight){
							   let scroller = new TableScroller($("#matTablepanel"), config.time*1000 , rowHeight);
							   scroller.start();
						}
					}
					merge(res,table_options);
                    //默认选择第一行
                    var rowIndex = 0;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i].LBQCPMat_Id == matTable.config.PK) {
                            rowIndex = res.data[i].LAY_TABLE_INDEX;
                            break;
                        }
                    }
                    if (config.delIndex != null) {
                        rowIndex = config.delIndex;
                        config.delIndex = null;
                    }
                    //默认选择第一行
                    doAutoSelect(table_options, rowIndex);
                },
                text: { none: '暂无相关数据' }
            };
            //标题
            if (options.title) {
                table_options.title = options.title;
            }
            if (options.url) {
                var fields = getStoreFields(table_options, true);
                table_options.url = options.url + '&fields=' + fields;
                table_options.initSort = options.initSort;
            }
			
            if (options.defaultToolbar) table_options.defaultToolbar = options.defaultToolbar;
            return uxtable.render(table_options);
        }
    };
	
	function TableScroller(tableContainer, interval , rowHeight1) {
	    // 响应鼠标事件
	    let that = this;
	    tableContainer.on('mouseover', function () {
	        that.pause();
	    });
	    tableContainer.on('mouseleave', function () {
	        that.resume();
	    });
	 
	    // 隐藏表格滚动条
	    let bodyContainer = tableContainer.find('.layui-table-body');
	    bodyContainer.css('overflow-y', 'hidden');
	 
	    this.timerID = null;
	    this.interval = interval;
	    this._bodyTable = bodyContainer.find('table');
	    this._tbody = this._bodyTable.find('tbody');
	 
	    this.start = function () {
	        let that = this;/* 
	        that.timerID = setInterval(function () { */
	            that._scroll(that._bodyTable, that._tbody, that.interval);/* 
	        }, that.interval); */
	    };
	    this.pause = function () {
	        let that = this;
	        if (that.timerID === null) {
	            return;
	        }
	        clearInterval(that.timerID);
	        that.timerID = null;
	    };
	    this.resume = function () {
	        let that = this;
	        if (that.timerID !== null || that.callback === null || that.interval === null) {
	            return;
	        }
	 
	        that.timerID = setInterval(function () {
	            that._scroll(that._bodyTable, that._tbody, that.interval);
	        }, that.interval);
	    };
	    this.stop = function () {
	        let that = this;
	        if (this.timerID === null) {
	            return;
	        }
	        clearInterval(that.timerID);
	        that.callback = null;
	        that.interval = null;
	        that.timerID = null;
	    };
	    this._scroll = function (bodyTable, tbody, interval) {
	        //let firstRow = tbody.find('tr:first');
			var mainheight = document.getElementsByClassName("layui-table-main")[0].offsetHeight;
	        let rowHeight = rowHeight1-mainheight;
	        bodyTable.animate({top: '-' + rowHeight + 'px'}, interval , function () {
	          
	        });
	    }
	};
	
    /**创建数据字段*/
    var getStoreFields = function (tableId, isString) {
        var columns = tableId.cols[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
            if (columns[i].field) {
                var obj = isString ? columns[i].field : {
                    name: columns[i].field,
                    type: columns[i].type ? columns[i].type : 'string'
                };
                fields.push(obj);
            }
        }
        return fields;
    };
    /***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
    var doAutoSelect = function (curTable, rowIndex) {
        curTable.key = curTable.id;
        var data = table.cache[curTable.key] || [];
        if (!data || data.length <= 0) return;
        rowIndex = rowIndex || 0;
        var tableDiv = $(curTable.elem + "+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        var filter = $(curTable.elem).find('lay-filter');
        var obj = {
            tr: thatrow,
            data: data[rowIndex] || {},
            del: function () {
                table.cache[curTable.key][index] = [];
                tr.remove();
                curTable.scrollPatch();
            },
            updte: {}
        };
        layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
        thatrow.click();
    };
	//数据加载-对外公开
	matTable.searchData = function (data,time,isScroll,limit,cursor,GoodsClassType) {
		config.GoodsClassType=GoodsClassType;
		config.cursor=cursor;
		config.limit=limit;
		config.time=time;
		config.isScroll=isScroll;
		var options = {
		    elem: '#matTable',
		    id: 'matTable',
		    title: '质控物',
		    height: 'full-140',
			limit:limit,
			url:""
		};
		matTable.render(options,data);
	};
	//数据加载-对外公开
	matTable.setIsScroll = function (isScroll) {
		config.isScroll=isScroll;
		var hql="&where=reagoods.GoodsClassType='"+config.GoodsClassType+"'";
		var url=config.selectUrl+hql+"&page=" + config.cursor + "&limit="+config.limit;
		uxutil.server.ajax({
			 url: url
		}, function (data) {
			if (data.success === true) {
				var ResultDataValue = $.parseJSON(data.ResultDataValue);
				matTable.searchData(ResultDataValue.list,config.time,config.isScroll,config.limit,config.cursor,config.GoodsClassType);
			}
		});
	}
    //数据加载-对外公开 
    matTable.onSearch = function (hql,pages) {
        var me = this;
		var url=config.selectUrl+hql;
        table.reload(config.elem, {
			url:url,
            where: {
                time: new Date().getTime()
            },
			page: {
			    curr: pages //重新从第 1 页开始
			}
        });
    };
	// 清除自身的localStorage里面的缓存
	matTable.clearCache = function(){
		soulTable.clearCache(config.elem);
	}
    //设置删除数据的所在位置-删除定位
    matTable.setDelIndex = function () {
        var me = this;
        config.delIndex = Number($(config.elem + "+div .layui-table-body table.layui-table tbody tr.layui-table-click").attr("data-index"));
    };
    //暴露接口
	
	// 合并列表
	function merge(res,table) {
		var me = this;
		var data = res.data;
		var mergeIndex = 0; //定位需要添加合并属性的行数
		var mark = 1; //这里涉及到简单的运算，mark是计算每次需要合并的格子数
		var columsName = ['ReaGoodsStockWarning_GoodsCName', 'ReaGoodsStockWarning_StockWarningByGoods']; //需要合并的列名称
		var columsIndex = getIndexColumn(); //需要合并的列索引值
		var LabNamemark = [];
		var tableid = table.id;
		for(var k = 0; k < columsName.length; k++) { //这里循环所有要合并的列
			var trArr = $("[lay-id="+ tableid +"] .layui-table-body>.layui-table").find("tr"); //所有行
			var flag = 0;
			for(var i = 1; i < res.data.length; i++) { //这里循环表格当前的数据
				var tdCurArr = trArr.eq(i).find("td").eq(columsIndex[k]); //获取当前行的当前列
				var tdPreArr = trArr.eq(mergeIndex).find("td").eq(columsIndex[k]); //获取相同列的第一列
				// var flag = 0;
				if(data[i][columsName[k]] === data[i - 1][columsName[k]]) { //后一行的值与前一行的值做比较，相同就需要合并
					mark += 1;
					if(columsName[k] != "ReaGoodsStockWarning_GoodsCName") {
						if(mark > LabNamemark[flag]) {
							flag += 1;
							mergeIndex = i;
							mark = 1;
							continue;
						}
					}
					tdPreArr.each(function() { //相同列的第一列增加rowspan属性
						$(this).attr("rowspan", mark);
					});
					tdCurArr.each(function() { //当前行隐藏
						$(this).css("display", "none");
					});
				} else {
					if(columsName[k] == "ReaGoodsStockWarning_GoodsCName") {
						LabNamemark.push(mark);
					} else {
						flag += 1;
					}
					mergeIndex = i;
					mark = 1; //一旦前后两行的值不一样了，那么需要合并的格子数mark就需要重新计算
				}
			}
			mergeIndex = 0;
			mark = 1;
		}
	};
	function getIndexColumn(){
		var columsIndex = [5,16]; //需要合并的列索引值
		var storeKey = location.pathname + location.hash + 'matTable';
		var curTableSession = localStorage.getItem('manualOpen'+storeKey);
		if(curTableSession&&curTableSession!="null"&&curTableSession!=null){
			curTableSession=$.parseJSON(curTableSession);
			for(var k in curTableSession[0]){
				if(curTableSession[0][k].field&&curTableSession[0][k].field=="ReaGoodsStockWarning_GoodsCName"){
					columsIndex[0]=k;
				}else if(curTableSession[0][k].field&&curTableSession[0][k].field=="ReaGoodsStockWarning_StockWarningByGoods"){
					columsIndex[1]=k;
				}
			}
		}
		return columsIndex;
	}
    exports('matTable', matTable);
});