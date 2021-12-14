/**
 * 医院区域字典
 * @author guohaixiang
 * @version 2019-12-11
 */

layui.extend({
    uxutil: 'ux/util',
    treeTable: 'ux/other/treeTable/treeTable',
}).use(['uxutil', 'table', 'form', 'treeTable'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
        treeTable = layui.treeTable,
        laytable=layui.table,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields:{
			Id:"BHospitalArea_Id",
			IsUse:'BHospitalArea_IsUse'
        },
        PHospitalAreaID: 0,
        IsAddALLNode:false,
        current: null,
        delIndex: null,
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchTreeGridBHospitalAreaByHQL?isPlanish=true&sort=[{property:"BHospitalArea_DispOrder",direction:"ASC"}]&where=IsUse=1',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalArea',
        newselectUrl:uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?isPlanish=true&sort=[{property:"BHospitalArea_DispOrder",direction:"ASC"}]&where=IsUse=1'
	};

	//初始化表格
    var inttb = function () {
       return treeTable.render({
            elem: '#table',
            height: 'full-1',
            size: 'sm', //小尺寸的表格
            tree: {
                iconIndex: 1,  // 折叠图标显示在第几列
                idName: 'Id',  // 自定义id字段的名称
                pidName: 'PHospitalAreaID',  // 自定义标识是否还有子节点的字段名称
                haveChildName: 'IsChild',   // 自定义标识是否还有子节点的字段名称
                // pid的字段名
                childName: 'ChildHosps', 
            },
            cols: [
                {
                    type: 'numbers', title: '序号', width: 45,
                },
                
                {
                    field: 'Name',
                    title: '区域名称',
                    minWidth: 130,
                    sort: true
                }, {
                    field: 'Id',
                    width: 160,
                    title: '主键ID',
                    hide: true
                },
                /*{
                    field: 'BHospitalArea_Code',
                    title: '医院区域简码',
                    minWidth: 130,
                    sort: true
                },*/
               /* {
                    field: 'PHospitalAreaID',
                    title: '父区域ID',
                    width: 160,
                    sort: true,
                    hide: true
                },
                {
                    field: 'PHospitalAreaName',
                    title: '父区域名称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'Code',
                    title: '父区域简码',
                    minWidth: 40,
                    sort: true
                },
                {
                    field: 'BHospitalArea_CenterHospitalID',
                    title: '区域中心医院ID',
                    minWidth: 130,
                    sort: true
                }*/
                /*{
                    field: 'CenterHospitalName',
                    title: '区域中心医院名称',
                    sort: true,
                    minWidth: 130
                }, {
                    field: 'DispOrder',
                    title: '次序',
                    minWidth: 30,
                    sort: true
                }, {
                    field: 'DeptName',
                    title: '部门',
                    minWidth: 50,
                    sort: true
                },
                 { align: 'center', toolbar: '#toolbar', title: '操作', width: 200 }
               ,
                {
                    field: 'BHospitalArea_AreaTypeID',
                    title: '区域类型ID',
                    sort: true,
                    minWidth: 130
                },
                {
                    field: 'BHospitalArea_AreaTypeName',
                    title: '区域类型名称',
                    sort: true,
                    minWidth: 130
                },
                {
                    field: 'BHospitalArea_SName',
                    title: '简称',
                    minWidth: 70,
                    //hide: true,
                    sort: true
                },
                {
                    field: 'BHospitalArea_Shortcode',
                    title: '快捷码',
                    minWidth: 100,
                    sort: true
                },
                {
                    field: 'BHospitalArea_Comment',
                    title: '备注',
                    minWidth: 100,
                    sort: true
                },               
                {
                    field: 'BHospitalArea_PinYinZiTou',
                    title: '拼音字头',
                    minWidth: 100,
                    sort: true
                }, 
                {
                    field: 'BHospitalArea_DispOrder',
                    title: '排序字段',
                    minWidth: 100,
                    sort: true
                }*/
            ],
            reqData: function (data, callback) {  // 懒加载也可以用url方式，这里用reqData方式演示
                var url = tableObj.selectUrl;
                if (data && data.Id!=0) {
                    url += " and Id = " + data.Id;
                } else {
                    url += " and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
                }
                $.get(url, function (res) {
                    var rd = JSON.parse(res.ResultDataValue).list;
                    if (tableObj.IsAddALLNode) {
                        callback(rd);
                    } else {
                        var rda = [{
                            Id: 0,
                            Name: '总节点',
                            IsChild: rd.length > 0,
                            iChildHosps: rd
                        }];
                        callback(rda);
                        tableObj.IsAddALLNode = true;
                        $("#table+div .ew-tree-table-box table.layui-table tbody tr:first-child")[0].click();
                        $("#table+div .ew-tree-table-box table.layui-table tbody tr:first-child td").find('.ew-tree-pack').trigger('click');
                    
                    }
                    //insTb.expand(1);
                });
                
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
            }
        });
    };
    inttbNew = function () {
        return laytable.render({
            elem: '#newtable',
            height: 'full-1',
            //size: 'sm', //小尺寸的表格
            //url:tableObj.newselectUrl,
            data:[],
            cols: [[
                {
                    type: 'numbers', title: '序号', width: 45,
                },
                {
                    field: 'BHospitalArea_Name',
                    title: '区域名称',
                    minWidth: 130,
                    sort: true
                }, {
                    field: 'BHospitalArea_Id',
                    width: 160,
                    title: '主键ID',
                    hide: true
                },{
                    field: 'BHospitalArea_PHospitalAreaID',
                     title: '父区域ID',
                     width: 160,
                     sort: true,
                     hide: true
                 },
                 {
                     field: 'BHospitalArea_PHospitalAreaName',
                     title: '父区域名称',
                     minWidth: 130,
                     sort: true
                 },
                 {
                     field: 'BHospitalArea_CenterHospitalID',
                     title: '区域中心医院ID',
                     minWidth: 130,
                     sort: true,
                     hide: true
                },
               {
                   field: 'BHospitalArea_CenterHospitalName',
                    title: '区域中心医院名称',
                    sort: true,
                    minWidth: 130
                }, {
                   field: 'BHospitalArea_DispOrder',
                    title: '次序',
                    minWidth: 30,
                }, {
                   field: 'BHospitalArea_DeptName',
                    title: '部门',
                    minWidth: 50,
                    sort: true
                },
                 { align: 'center', toolbar: '#toolbar', title: '操作', width: 200 }
            ]],
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            }
        });
    };
    
    var insTb = inttb();
    var newinsTb = inttbNew();
    //table上面的工具栏
    laytable.on('tool(newtable)', function (obj) {
        switch (obj.event) {
            case 'edit':
                parent.layer.open({
                    type: 2,
                    area: ['1000px', '692px'],
                    fixed: false,
                    maxmin: false,
                    title: '编辑区域医院',
                    content: uxutil.path.ROOT + '/ui/layui/views/system/Hospital/B_HospitalArea_New/New_areaHospitalForm/hospitalAreaForm.html?AreaId=' + obj.data.BHospitalArea_Id + '&PAreaId=' + obj.data.BHospitalArea_PHospitalAreaID + '&type=edit',
                    cancel: function (index, layero) {
                        var url = tableObj.newselectUrl;
                        if (tableObj.PHospitalAreaID != 0) {
                            url += " and PHospitalAreaID = " + obj.data.Id;
                        }
                        else {
                            url += " and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
                        }
                        newinsTb.reload({ url: url });
                    },
                    success: function (layero, index) {
                    }
                });
                break;
            case 'del':
                layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                    var loadIndex = layer.load();//开启加载层
                    $.ajax({
                        type: "get",
                        url: tableObj.delUrl + "?Id=" + obj.data.BHospitalArea_Id,
                        async: true,
                        dataType: 'json',
                        success: function (res) {
                            if (res.success) {
                                layer.close(loadIndex);//关闭加载层
                                layer.close(index);
                                layer.msg("删除成功！", { icon: 6, anim: 0 });
                                var url = tableObj.newselectUrl;
                                if (tableObj.PHospitalAreaID != 0) {
                                    url += " and PHospitalAreaID = " + obj.data.Id;
                                }
                                else {
                                    url += " and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
                                }
                                newinsTb.reload({ url: url });
                            } else {
                                layer.msg("删除失败！\r\n"+res.ErrorInfo, { icon: 5, anim: 6 });
                                tableObj.delIndex = null;
                                layer.close(loadIndex);//关闭加载层
                            }
                        }
                    });
                });
                break;

        };
    });

    $('#newtable_btnSearch').click(function () {
        var keywords = $('#newtable_edtSearch').val();
        if (keywords) {
            var url = tableObj.newselectUrl;
            url += " and PHospitalAreaID = " + tableObj.PHospitalAreaID + " and Name='" + keywords+"'";
            newinsTb.reload({ url: url });
        } else {
            var url = tableObj.newselectUrl;
            url += " and PHospitalAreaID = " + tableObj.PHospitalAreaID ;
            newinsTb.reload({ url: url });
        }
    });
    
    $('#newtable_formAdd').click(function () {
        parent.layer.open({
            type: 2,
            area: ['1000px', '692px'],
            fixed: false,
            maxmin: false,
            title: '区域新增医院',
            content: uxutil.path.ROOT + '/ui/layui/views/system/Hospital/B_HospitalArea_New/New_areaHospitalForm/hospitalAreaForm.html?AreaId=' + tableObj.PHospitalAreaID + '&type=add',
            cancel: function (index, layero) {
                var url = tableObj.newselectUrl;
                if (tableObj.PHospitalAreaID != 0) {
                    url += " and PHospitalAreaID = " + obj.data.Id;
                }
                else {
                    url += " and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
                }
                newinsTb.reload({ url: url });
            }
        });
    });

    // 监听行单击事件
    treeTable.on('row(table)', function (obj) {
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        tableObj.PHospitalAreaID = obj.data.Id;
        var url = tableObj.newselectUrl;
        if (tableObj.PHospitalAreaID !=0) {
            url += " and PHospitalAreaID = " + obj.data.Id;
        }
        else {
            url += " and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
        }
        newinsTb.reload ({url:url});

    });
    // 全部展开
    $('#btnExpandAll').click(function () {
        insTb.expandAll();
    });

    // 全部折叠
    $('#btnFoldAll').click(function () {
        insTb.foldAll();
    });

    $('#btnSearch').click(function () {
        var keywords = $('#edtSearch').val();
        if (keywords) {
            insTb.filterData(keywords);
        } else {
            insTb.clearFilter();
        }
    });

    $('#btnClearSearch').click(function () {
        insTb.clearFilter();
        $("#edtSearch").val("");
    });
});