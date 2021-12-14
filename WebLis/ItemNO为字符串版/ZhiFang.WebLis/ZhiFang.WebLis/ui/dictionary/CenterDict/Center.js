
var Dictionary;
var firstLoad = 0;
$(function () {
    //中心项目字典表

    $('#selectDict').combobox({
        valueField: 'value',
        textField: 'text',
        panelheight: 215,
        editable: false,
        data: [
            { value: 0, text: '中心项目字典表' },
            { value: 1, text: '中心样本类型字典表' },
            { value: 2, text: '中心民族字典表' },
            { value: 3, text: '中心检验大组字典表' },
            { value: 4, text: '中心性别字典表' },
            { value: 5, text: '中心就诊类型字典表' },
            { value: 6, text: '中心医疗机构字典表' },
            { value: 7, text: '中心区域字典表' },
            { value: 8, text: '中心医生字典表' },
            { value: 9, text: '中心病区字典表' },
            { value: 10, text: '中心科室字典表' },
            { value: 11, text: '中心检验小组字典表' },
            { value: 12, text: '中心颜色字典表' },
            { value: 13, text: '中心体检类型字典表' }
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);

        },
        onSelect: function (record) {
            switch (record.value) {
                case 0:
                    $('#Dict').attr('src', 'ItemDict/grid/ItemDict.html'); break;
                case 1:
                    $('#Dict').attr('src', 'SampleTypeDict/grid/SampleTypeDict.html'); break;
                case 2:
                    $('#Dict').attr('src', 'FolkTypeDict/grid/FolkTypeDict.html'); break;
                case 3:
                    $('#Dict').attr('src', 'SuperGroupDict/grid/SuperGroupDict.html'); break;
                case 4:
                    $('#Dict').attr('src', 'GenderTypeDict/grid/GenderTypeDict.html'); break;
                case 5:
                    $('#Dict').attr('src', 'SickTypeDict/grid/SickTypeDict.html'); break;
                case 6:
                    $('#Dict').attr('src', 'CLIENTELEDict/grid/CLIENTELEDict.html'); break;
                case 7:
                    $('#Dict').attr('src', 'ClientEleAreaDict/grid/ClientEleAreaDict.html'); break;
                case 8:
                    $('#Dict').attr('src', 'DoctorDict/grid/DoctorDict.html'); break;
                case 9:
                    $('#Dict').attr('src', 'DistrictDict/grid/DistrictDict.html'); break;
                case 10:
                    $('#Dict').attr('src', 'Department/grid/Department.html'); break;
                case 11:
                    $('#Dict').attr('src', 'PGroupDict/grid/PGroupDict.html'); break;
                case 12:
                    $('#Dict').attr('src', 'ColorDict/centerColor.html'); break;
                case 13:
                    $('#Dict').attr('src', 'BPhysicalExamTypeDict/grid/BPhysicalExamTypeDict.html'); break;

            }
        }
    });
});

