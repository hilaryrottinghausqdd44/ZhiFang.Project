unit uMicroServiceInf;

interface

uses
  SynCommons, mORMot;

type
  IMicroQuery = interface(iinvokable)
    ['{AF0B1D33-4886-4568-A20D-ADDF0902BAE5}']
    function GetServerTime:string;
    function ShowMainFormData: RawUTF8;
    function SearchMainFormData(MainState,StartDate,EndDate,SectionNo,CName,PatNo,CardNo,SampleTypeNo,SickTypeNo,
    SerialNo,GSampleNo,ItemNo,MicroID,AntiID:string): RawUTF8;
    function GetPeopleInfo(GSFID,GTestDate:string): RawUTF8;
    function GetMicroTestItem(bALL:Boolean=False): RawUTF8;
    function GetMicroSection(SectionNo:string;bALL:Boolean=False): RawUTF8;
    function GetSampleType(bALL:Boolean=False): RawUTF8;
    function GetSickType: RawUTF8;
    function GetMicro(bALL:Boolean=False;bHaveALLOptions:Boolean=False): RawUTF8;
    function GetAnti(bALL:Boolean=False;bHaveALLOptions:Boolean=False): RawUTF8;
  end;

implementation

initialization
  TInterfaceFactory.RegisterInterfaces([TypeInfo(IMicroQuery)]);

end.

