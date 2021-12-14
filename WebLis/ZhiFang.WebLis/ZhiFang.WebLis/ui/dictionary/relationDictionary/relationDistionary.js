/**
 * @OverView 对照关系字典导航模板
 * Created by Administrator on 2015/1/7.
 */

//对照关系表
var controlDictionary;
//实验室
var labCode;
//首次加载页面
var firstLoad=0;


//程序入口
$(function(){
    setCombobox();
});

//设置下拉列表框的默认值
function setCombobox() {

    //对照关系列表
    $('#cmbLabList').combobox({
        valueField: 'value',
        textField: 'text',
        editable:false,
        panelHeight:215,
        data: [
            {value: 0, text: '申请项目字典表对照关系表'},
            {value: 1, text: '样本类型字典表对照关系表'},
            {value: 2, text: '民族字典表对照关系表'},
            {value: 3, text: '检验大组字典表对照关系表'},
            {value: 4, text: '性别字典表对照关系表'},
            {value: 5, text: '就诊类型字典表对照关系表'},
            {value: 6, text: '病区字典表对照关系表'},
            {value: 7, text: '医生字典表对照关系表'},
            {value: 8, text: '科室字典表对照关系表'},
            {value: 9, text: '检验小组字典表对照关系表'},
            {value: 10, text: '颜色样本类型对照关系表'},
            {value: 11, text: '结果项目字典表对照关系表'}
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);
            controlDictionary='projectRelation.html';
        },
        onSelect:function(record){
            switch (record.value){
                case 0:controlDictionary='projectRelation.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                        $('#controlContainer').attr('src','projectRelation.html'+'?labCode='+labCode);break;
                    }

                case 1:controlDictionary='SampleTypeControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','SampleTypeControl.html'+'?labCode='+labCode);break;
                    }

                case 2:controlDictionary='FolkTypeControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','FolkTypeControl.html'+'?labCode='+labCode);break;
                    }

                case 3:controlDictionary='SuperGroupControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','SuperGroupControl.html'+'?labCode='+labCode);break;
                    }

                case 4:controlDictionary='GenderTypeControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','GenderTypeControl.html'+'?labCode='+labCode);break;
                    }

                case 5:controlDictionary='SickTypeControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','SickTypeControl.html'+'?labCode='+labCode);break;
                    }

                case 6:controlDictionary='DistrictControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','DistrictControl.html'+'?labCode='+labCode);break;
                    }

                case 7:controlDictionary='DoctorControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','DoctorControl.html'+'?labCode='+labCode);break;
                    }

                case 8:controlDictionary='DepartmentControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','DepartmentControl.html'+'?labCode='+labCode);break;
                    }

                case 9:controlDictionary='PGroupControl.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                    $('#controlContainer').attr('src','PGroupControl.html'+'?labCode='+labCode);break;
                    }

                case 10:controlDictionary='SampleColorControl.html';

                        $('#controlContainer').attr('src','SampleColorControl.html');
                        $('#cmbLab').combobox('disable');break;

                case 11:controlDictionary='resultProjectRelation.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#cmbLab').combobox('enable');
                        $('#controlContainer').attr('src','resultProjectRelation.html'+'?labCode='+labCode);break;
                    }
            }

        }
    });

    //实验室
    $('#cmbLab').combobox({
        url:Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetPubDict?tablename=CLIENTELE&fields=ClIENTNO,CNAME',
        method:'GET',
        valueField: 'ClIENTNO',
        textField: 'CNAME',
        loadFilter:function(data){
            data=eval('('+data.ResultDataValue+')').rows || [];//eval()把字符串转换成JSON格式
            return data;
        },
        onLoadSuccess: function () {
            var data = $(this).combobox('getData');
            if (data.length > 0) {
                $(this).combobox('select', data[0].ClIENTNO);//默认第一项的值
                labCode=data[0].ClIENTNO;
            }
        },
        filter:function(q,row){
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1;//返回true,则显示出来
        },
        onSelect:function(record){
           labCode=record.ClIENTNO;
            $('#controlContainer').attr('src',controlDictionary+'?labCode='+labCode);

        }
    });

    $('#cmbLab').combobox('enable');
}