/**
 * @OverView 实验室字典导航模板
 * Created by Administrator on 2015/1/7.
 */

//实验室字典表
var labDictionary;
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

    //实验室字典列表
    $('#cmbLabList').combobox({
        valueField: 'value',
        textField: 'text',
        editable:false,
        panelHeight:215,
        data: [
            {value: 0, text: '实验室项目字典表'},
            {value: 1, text: '实验室样本类型字典表'},
            {value: 2, text: '实验室民族字典表'},
            {value: 3, text: '实验室检验大组字典表'},
            {value: 4, text: '实验室性别字典表'},
            {value: 5, text: '实验室就诊类型字典表'},
            {value: 6, text: '实验室病区字典表'},
            {value: 7, text: '实验室医生字典表'},
            {value: 8, text: '实验室科室字典表'},
            {value: 9, text: '实验室检验小组字典表'}
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);
        },
        onSelect:function(record){
            switch (record.value){
                case 0:labDictionary='LabTestItem.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabTestItem.html'+'?labCode='+labCode);break;
                    }

                case 1:labDictionary='LabSampleType.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabSampleType.html'+'?labCode='+labCode);break;
                    }

                case 2:labDictionary='LabFolkType.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabFolkType.html'+'?labCode='+labCode);break;
                    }

                case 3:labDictionary='LabSuperGroup.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabSuperGroup.html'+'?labCode='+labCode);break;
                    }

                case 4:labDictionary='LabGenderType.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabGenderType.html'+'?labCode='+labCode);break;
                    }

                case 5:labDictionary='LabSickType.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabSickType.html'+'?labCode='+labCode);break;
                    }

                case 6:labDictionary='LabDistrict.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabDistrict.html'+'?labCode='+labCode);break;
                    }

                case 7:labDictionary='LabDoctor.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabDoctor.html'+'?labCode='+labCode);break;
                    }

                case 8:labDictionary='LabDepartment.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabDepartment.html'+'?labCode='+labCode);break;
                    }

                case 9:labDictionary='LabPGroup.html';
                    if(firstLoad===0){
                        firstLoad++;break;}else{
                        $('#labContainer').attr('src','LabPGroup.html'+'?labCode='+labCode);break;
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
            }
        },
        filter:function(q,row){
            var opts = $(this).combobox('options');
            return row[opts.textField].indexOf(q) > -1;//返回true,则显示出来
        },
        onSelect:function(record){
            labCode=record.ClIENTNO;
            $('#labContainer').attr('src',labDictionary+'?labCode='+labCode);

        }
    });
}