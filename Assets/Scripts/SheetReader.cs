using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;

class SheetReader
{
    static private String spreadsheetId = "11GX9b5pqd_WXhnozeA2iJOcZp7Lq63NXU61j5c3i8lY";
    static private String jsonPath = "/StreamingAssets/Credentials/cannon-21512-fd13df973d76.json";

    static private SheetsService service;

    static SheetReader()
    {
        String fullJsonPath = Application.dataPath + jsonPath;

        Stream jsonCreds = (Stream)File.Open(fullJsonPath, FileMode.Open);

        ServiceAccountCredential credential = ServiceAccountCredential.FromServiceAccountData(jsonCreds);

        service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
        });
    }

    public IList<IList<object>> getSheetRange(String sheetNameAndRange)
    {
        SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheetNameAndRange);

        ValueRange response = request.Execute();
        IList<IList<object>> values = response.Values;
        if (values != null && values.Count > 0)
        {
            return values;
        }
        else
        {
            Debug.Log("No data found.");
            return null;
        }
    }
}

