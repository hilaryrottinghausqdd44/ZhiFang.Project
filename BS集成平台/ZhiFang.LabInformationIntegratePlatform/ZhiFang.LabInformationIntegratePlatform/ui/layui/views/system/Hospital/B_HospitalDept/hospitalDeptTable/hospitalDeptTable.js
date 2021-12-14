/**
	@name：医院科室列表
	@author：guohaixiang
	@version 2019-12-12
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;

    var config = { id: null,name:null };

    var HospitalDeptTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData: [],
            /**默认传入参数*/
            defaultParams: {},
            /**默认数据条件*/
            defaultWhere: '',
            /**内部数据条件*/
            internalWhere: '',
            /**外部数据条件*/
            externalWhere: '',
            /**是否默认加载*/
            defaultLoad: false,
            /**列表当前排序*/
            /*sort: [{
                "property": '',
                "direction": 'DESC'
            }],*/
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalDeptByHQL?isPlanish=true',
            where: "",
            defaultToolbar: ['filter'],
            toolbar: '#depttoolbar',
            page: true,
            limit: 50,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            //size: 'sm', //小尺寸的表格
            cols: [[
                {
                    field: 'BHospitalDept_Id',
                    width: 60,
                    title: '主键ID',
                    sort: true,
                    hide: true
                },
                {
                    field: 'BHospitalDept_HospitalID',
                    title: '医院字典ID',
                    minWidth: 130,
                    hide:true 
                    //sort: true
                },
                {
                    field: 'BHospitalDept_HospitalName',
                    title: '医院名称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalDept_Name',
                    title: '名称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalDept_SName',
                    title: '简称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalDept_Shortcode',
                    title: '快捷码',
                    minWidth: 130,
                    //sort: true
                },
                {
                    field: 'BHospitalDept_PinYinZiTou',
                    title: '拼音字头',
                    minWidth: 130,
                    //sort: true
                },
                {
                    field: 'BHospitalDept_IsUse',
                    title: '是否使用',
                    minWidth: 100,
                    //sort: true,
                    templet:function(data){
                    	var str = "";
                    	//console.log(data);
                    	if(data.BHospitalDept_IsUse.toString() == "true"){
                    		str = "<span style='color:red;'>是</span>";
                    	}else{
                    		str = "<span>否</span>";
                    	}
                    	return str;
                    }
                }
            ]],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                //无数据处理
                if (count == 0) {
                    //layui.event("", "data", {  });
                    return;
                }
                //触发点击事件
                $("#HospitalDeptTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, HospitalDeptTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, HospitalDeptTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
        arr.push("HospitalID='"+config.id+"'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JSON.stringify(where);
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取查询Fields
    Class.pt.getStoreFields = function (isString) {
        var me = this,
            columns = me.config.cols[0] || [],
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
    //核心入口
    HospitalDeptTable.render = function (options,where) {
        var me = this;
        config.id=where.id;
        config.name=where.name;
        var inst = new Class(options);
        inst.config.url = inst.getLoadUrl();
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    HospitalDeptTable.onSearch = function (mytable, where) {
        var me = this;
        config.id=where.id;
        config.name=where.name;
        var inst = new Class(me);
        HospitalDeptTable.url = inst.getLoadUrl();
        HospitalDeptTable.elem = "#" + mytable;
        HospitalDeptTable.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function ($) {
        //获取当前窗口的宽度
        var width = $(window).width();
        if (width > 1200) {
            return 3;   //大屏幕
        } else if (width > 992) {
            return 2;   //中屏幕
        } else if (width > 768) {
            return 1;   //小屏幕
        } else {
            return 0;   //超小屏幕
        }
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(HospitalDeptTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            //layui.event("HospitalTableClick", "click", { id: obj.data.LBQCRuleBase_Id });
        });
        //监听排序事件
        table.on('sort(HospitalDeptTable)', function (obj) {
            var field = obj.field;//排序字段
            var type = obj.type;//升序还是降序
            //修改默认的排序字段
            me.config.sort = [{
                "property": field,
                "direction": type
            }];
            table.reload('HospitalDeptTable', {
                initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: me.getLoadUrl(),
                where: {
                    time: new Date().getTime()
                }
            });
        });
        //table上面的工具栏
		table.on('toolbar(HospitalDeptTable)', function(obj) {
			//console.log(me.config.checkRowData);
			switch(obj.event) {
				case 'add':
		            var flag = false;
		            parent.layer.open({
		                type: 2,
		                area: ['800px', '500px'],
		                fixed: false,
		                maxmin: false,
		                title: '新增科室',
		                content: uxutil.path.ROOT + '/ui/layui/views/system/Hospital/B_HospitalDept/hospitalDeptForm/hospitalDeptForm.html?HospitalId=' + config.id+ "&HospitalName=" + config.name+"&formType=add",
		                cancel: function (index, layero) {
		                    flag = true;
		                },
		                success: function (layero, index) {
		                    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
		                    body.find('#HospitalName').val(config.name);
		                    body.find('#HospitalID').val(config.id);
		                },
		                end: function () {
		                    if (flag) return;
		                    table.reload(me.config.id);
		                }
		            });
					break;
				case 'edit':
					var flag = false;
		            parent.layer.open({
		                type: 2,
		                area: ['800px', '500px'],
		                fixed: false,
		                maxmin: false,
		                title: '修改科室',
		                content: uxutil.path.ROOT + '/ui/layui/views/system/Hospital/B_HospitalDept/hospitalDeptForm/hospitalDeptForm.html?HospitalDeptId=' + me.config.checkRowData[0]["BHospitalDept_Id"]+"&formType=edit",
		                cancel: function (index, layero) {
		                    flag = true;
		                },
		                success: function (layero, index) {
		                    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
		                },
		                end: function () {
		                    if (flag) return;
		                    table.reload(me.config.id);
		                }
		            });
					break;
				case 'del':
					if(me.config.checkRowData.length === 0) {
						layer.msg('请选择一行！');
	               } else {
	                    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
	                        var loadIndex = layer.load();//开启加载层
							var len = me.config.checkRowData.length;
						  	for(var i = 0; i < me.config.checkRowData.length; i++){
						  		var Id = me.config.checkRowData[i]["BHospitalDept_Id"];
						  		$.ajax({
							  		type:"get",
							  		url:uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalDept'+"?Id="+Id,
							  		async:true,
							  		dataType:'json',
	                                success: function (res) {
							  			if(res.success){
							  				len--;
	                                        if (len == 0) {
	                                            layer.close(loadIndex);//关闭加载层
								  				layer.close(index);
												layer.msg("删除成功！",{icon: 6,anim:0});
	                                            //refresh();
	                                            table.reload(me.config.id);
								  			}
							  			}else{
							  				layer.msg("删除失败！", { icon: 5, anim: 6 });
	                                        delIndex = null;
	                                        layer.close(loadIndex);//关闭加载层
							  			}
							  		}
							  	});
						  	}
						});
					}
					break;
				case 'deptsearch':
					if($("#deptsearch")[0].value == ""){
						table.reload('table',{
	                        url: selectUrl,
	                        where: {
	                            time: new Date().getTime()
	                        }
						});
						me.config.checkRowData = [];
					}else{
						var val = $("#search")[0].value;
						var url = "";
	                    if (selectUrl.indexOf("where") != -1) {
	                        var where = " and Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
	                        url = selectUrl.replace(')', where);
	                    } else {
	                        url = encodeURI(selectUrl + "&where=Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'");
	                    }
						table.reload('table',{
	                        url: url,
	                        where: {
	                            time: new Date().getTime()
	                        }
						});
						me.config.checkRowData = [];
						$("#search").val(val);
					}
			};
		});
    };
    
    //暴露接口
    exports('HospitalDeptTable', HospitalDeptTable);
});
