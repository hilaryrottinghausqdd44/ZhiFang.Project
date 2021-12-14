layui.extend({
    uxutil: 'ux/util',
    uxtable: 'ux/table'
}).use(['uxutil', 'uxtable', 'form'], function () {
    var $ = layui.$,
        form = layui.form,
        laydate = layui.laydate,
        uxtable = layui.uxtable,
        uxutil = layui.uxutil;

    //获取人员列表
    //http://localhost/ZhiFang.LabInformationIntegratePlatform/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?page=1&limit=10&fields=RBACUser_Account,RBACUser_HREmployee_CName,RBACUser_HREmployee_StandCode,RBACUser_HREmployee_Id&where=IsUse=1&sort=[{"property":"RBACUser_DispOrder","direction":"asc"}]&isPlanish=true
    var GET_EMP_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
    //获取待选/已选医院列表
    var GET_UNSELECTHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSelectHospitalListByEmpId";
    //删除员工医院关系服务
    var DEL_EMPHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalEmpLink";
    //员工医院关系服务
    var SetLinkType_EMPHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/BHospitalEmpLinkSetLinkType";
    //新增员工医院关系界面
    var UnSelectHospital_URL = "UnSelectHospital.html";
	//获取医院列表
	var GET_HOSPITAL_URL = uxutil.path.ROOT+'/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL';
	//查询医院人员关系列表
	var GET_HOSPITALEMPLINK_URL  = uxutil.path.ROOT+'/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL';
	var GET_HOSEMP_URL =  uxutil.path.ROOT+ '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkByHQL?isPlanish=true&page=1&limit=100000'
    var tmpempid = "";//医院ID
	var empdata = "";
    //人员列表
    var config_table_emp = {
        elem: $("#tableemp"),
        toolbar: '#toolbarEmp',
        height: 'full-50',
        defaultLoad: true,
        page: {
            limit: 50,
            limits: [50, 100, 200]
        },
        initSort: {
            field: 'BHospital_Name',//排序字段
            type: 'asc'
        },
        cols: [[
            { field: 'BHospital_Id', width: 1, title: '医院Id', hide: true },
            { field: 'BHospital_Name', width: '50%', title: '医院名称', sort: true },
            { field: 'BHospital_HospitalCode', width: '50%', title: '英文编码', sort: true },
            { field: 'BHospital_Shortcode', width: '30%', title: '快捷码', sort: true, hide: true }
        ]],
		done: function(res, curr, count) {
			if(count > 0) {
				$("#tableemp+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
			}
		},
    };
    var tableInd = uxtable.render(config_table_emp);
    //头工具栏事件
    tableInd.table.on('toolbar(tableemp)', function (obj) {
        switch (obj.event) {
            case 'search': onSearch(); break;
        }
    });
    //监听医院行点击事件
    tableInd.table.on('row(tableemp)', function (obj) {
		 obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        loadHospitalList(obj.data);
    });


    //
    var config_table_hospital = {
        elem: $("#tablehospital"),
        id: "tablehospital",
        toolbar: '#toolbarHospital',
        height: 'full-50',
        defaultLoad: true,
        page: {
            limit: 50,
            limits: [50, 100, 200]
        },
        /* initSort: {
            field: 'BHospitalEmpLink_HospitalCode',//排序字段
            type: 'asc'
        }, */
        cols: [[
            { type: 'checkbox' },
            { field: 'Id', width: 1, title: '关系Id', hide: true },
            { field: 'EmpID', width: '45%', title: '人员ID', sort: true, hide: true },
            { field: 'EmpName', width: '30%', title: '人员名称', sort: true },
			{ field: 'Account', width: '30%', title: '登录账号', sort: true },
			{ field: 'HospitalCode', width: '30%', title: '医院编码', sort: true, hide: true },
            {
                field: "LinkTypeName",
                title: '关系类型',
                minWidth: 100,
                templet: '#switchTp',
                unresize: true
            },
            { fixed: 'right', title: '操作', toolbar: '#RowBtn', width: 80 },
            { field: 'HospitalID', width: 100, title: '医院Id', hide: true }
        ]],
		parseData: function(res) { //res即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			empdata = data;
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.length || 0, //解析数据长度
				"data": data || []
			};
		},
    };
    var tableInd_hospital = uxtable.render(config_table_hospital);
    //头工具栏事件
    tableInd_hospital.table.on('toolbar(tablehospital)', function (obj) {
        console.log(obj);
        switch (obj.event) {
            case 'search': onSearch(); break;
            case 'add': addhospital(); break;
            case 'alldel': alldelhospital(); break;
        }
    });
    function alldelhospital() {
        var checkStatus = tableInd_hospital.table.checkStatus('tablehospital'); 
        var clns = checkStatus.data.length;
        if (clns<=0) {
            layer.msg('请选择数据！');
        } else {
            var index = 0;
            for (var i = 0; i < checkStatus.data.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: DEL_EMPHOSPITAL_URL + '?id=' + checkStatus.data[i].Id,
                    success: function (data) {
                        index++;
                        if (index==i) {
                            loadHospitalListByEmpId(tmpempid);
                        }
                    }
                });
            }
        }
    };
    //监听行工具事件
    tableInd_hospital.table.on('tool(tablehospital)', function (obj) {
        switch (obj.event) {
            case 'del': layer.confirm('确定是否删除？', function (index) {
                //alert(obj.data.BHospitalEmpLink_Id);
                delhospitalemplink(obj.data.Id);
            }); break;
        }
    });

    //监听切换关系类型操作
    form.on('switch(LinkType)', function (obj) {
		var values = this.value.split('_');
		var isok = true;
		$.ajax({
		    type: 'get',
			async: false,
		    contentType: 'application/json',
		    url: GET_HOSEMP_URL + '&fields=BHospitalEmpLink_LinkTypeID,BHospitalEmpLink_HospitalName&where=Id <> '+values[0]+' and EmpID='+values[1],
		    success: function (data) {
		        if (data.success == true) {
					var rdata = data.ResultDataValue ? $.parseJSON(data.ResultDataValue) : {};
					for (var i=0;i<rdata.count;i++) {
						if(rdata.list[i].BHospitalEmpLink_LinkTypeID == 1){
							layer.msg('此人员已所属医院：'+rdata.list[i].BHospitalEmpLink_HospitalName);
							isok = false;
							return false;
						}
					}
		        } else {
		            
		        }
		    }
		}); 
		if(isok){
			layer.tips(values[0] + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
			var typeid = (obj.elem.checked) ?'1':'2';
			$.ajax({
			    type: 'get',
			    contentType: 'application/json',
			    url: SetLinkType_EMPHOSPITAL_URL + '?id=' + values[0] + '&typeid=' + typeid,
			    success: function (data) {
			        if (data.success == true) {
			            layer.msg('操作成功！');
			        } else {
			            layer.msg('操作失败！');
			        }
			    }
			}); 
		}else{
			loadHospitalListByEmpId(tmpempid);
		}
        
    });

    //查询
    function onSearch() {
        var searchText = $("#searchtextemp").val(), where = [];

        //条件
        where.push(" IsUse=1 ");
        if (searchText && searchText != "")
            where.push(" (bhospital.Name like '%" + searchText + "%')");

        loadEmpList({ "where": where.join(' and ') });

    };
    //添加人员医院关系页面
    function addhospital() {
        var hosid = tmpempid;
        if (hosid == 0 || hosid == "") {
            layer.msg('请选择左侧医院后再添加人员！');
        }		
        else {
			var empids = [];
			if(empdata != ""){
				for(var i = 0;i < empdata.length;i++){
					empids.push(empdata[i].EmpID);
				}
			}
			var empid = empids.length > 0 ? empids.join(',') : "";
            var url = UnSelectHospital_URL;
            if (url) {
                layer.open({
                    title: '选择医院',
                    type: 2,
                    content: url + '?EMPID=' + hosid +'&EMPIDS='+empid + '&t=' + new Date().getTime(),
                    maxmin: true,
                    toolbar: true,
                    resize: true,
                    area: ['850px', '500px']
                });
            }
        }
    };

    //获取医院列表
    function loadEmpList(whereObj, callback) {

        var cols = config_table_emp.cols[0],
            fields = [];

        for (var i in cols) {
            fields.push(cols[i].field);
        }

        tableInd.reload({
            url: GET_HOSPITAL_URL,
            where: $.extend({}, whereObj, {
                isPlanish: true,
                fields: fields.join(',')
            })
        });
    };

    function loadHospitalList(empdata, callback) {

        if (empdata && empdata.BHospital_Id != "") {
            tmpempid = empdata.BHospital_Id;
            loadHospitalListByEmpId(empdata.BHospital_Id);
        }
    };

    function loadHospitalListByEmpId(empid, callback) {

        var cols = config_table_hospital.cols[0],
            fields = [];

        for (var i in cols) {
            fields.push(cols[i].field);
        }
        var eid = 0;
        if (empid && empid!= "") {
            eid = empid;
        }
        tableInd_hospital.reload({
            url: GET_HOSPITALEMPLINK_URL + '?v=' + uxutil.Random.generateMixed(20),
            where: $.extend({}, "", {
                isPlanish: true,
                fields: fields.join(','),
				sort:'[{"property":"BHospitalEmpLink_HospitalCode","direction":"ASC"}]',
                where: "bhospitalemplink.HospitalID="+eid
            })
        });
    };

    function delhospitalemplink(hospitalemplinkid) {
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: DEL_EMPHOSPITAL_URL + '?id=' + hospitalemplinkid,
            success: function (data) {
                if (data.success == true) {
                    layer.msg('操作成功！');
                    loadHospitalListByEmpId(tmpempid);
                } else {
                    layer.msg('所属医院关系删除失败！');
                }
            }
        });
    };
    //初始化
    function init() {
        onSearch();
        loadHospitalList("");
        window.loadHospitalListByEmpId = loadHospitalListByEmpId;
    };

    //初始化
    init();
});