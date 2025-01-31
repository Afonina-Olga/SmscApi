﻿using static SmsCenter.Api.Models.SmsCenter;

namespace SmsCenter.Api.Providers;

internal interface ISmsCenterProvider
{
    #region Отправка смс https: //smsc.ru/api/http/send

    /// <summary>
    /// Отправка одного и того же сообщения смс на один или несколько номеров
    /// </summary>
    /// <param name="phones">Телефонные номера, разделенные запятой</param>
    /// <param name="message">Сообщение</param>
    /// <param name="options">Дополнительные параметры запроса</param>
    ValueTask<Sms.Response> SendSms(
        string phones,
        string message,
        Sms.AdditionalOptions? options = default);

    /// <summary>
    /// Отправка различных сообщений смс на несколько номеров
    /// </summary>
    /// <param name="phoneAndMessageList">Список номеров телефонов и соответствующих им сообщений, разделенных двоеточием или точкой с запятой</param>
    /// <param name="options">Дополнительные параметры запроса</param>
    ValueTask<Sms.Response> SendSms(
        string phoneAndMessageList,
        Sms.AdditionalOptions? options = default);

    /// <summary>
    /// Получить стоимость отправки смс
    /// </summary>
    /// <param name="phones">Телефонные номера, разделенные запятой</param>
    /// <param name="message">Сообщение</param>
    ValueTask<Sms.Response> GetSmsSendingCost(string phones, string message);

    /// <summary>
    /// Получить стоимость отправки смс
    /// </summary>
    /// <param name="phoneAndMessageList">Список номеров телефонов и соответствующих им сообщений, разделенных двоеточием или точкой с запятой</param>
    /// <returns></returns>
    ValueTask<Sms.Response> GetSmsSendingCost(string phoneAndMessageList);

    /// <summary>
    /// Отправка hlr заголовка для проверки доступности номера
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    ValueTask<Sms.Response> SendHlr(string phone);

    ValueTask<Sms.Response> SendPing(string phone);

    #endregion

    /// <summary>
    /// Получение баланса
    /// </summary>
    /// <param name="currency">Валюта</param>
    /// <returns></returns>
    ValueTask<Balance.Response> GetBalance(byte? currency = default);

    /// <summary>
    /// Статус доставки смс сообщения
    /// </summary>
    /// <param name="phone">Номер телефона или список номеров через запятую при запросе статусов нескольких SMS.</param>
    /// <param name="id">
    /// Идентификатор сообщения или список идентификаторов через
    /// запятую при запросе статусов нескольких сообщений.
    /// </param>
    ValueTask<Status.SmsResponse> GetSmsStatus(string phone, int id);

    // Статус проверки доступности номера
    ValueTask<Status.HlrResponse> GetHlrStatus(string phone, int id);
    
    /// <summary>
    /// Удаление сообщения
    /// </summary>
    /// <param name="phone">Номер телефона</param>
    /// <param name="id">Идентификатор сообщения</param>
    ValueTask<Status.DeleteResponse> DeleteSms(string phone, int id);
    
    // Получение истории отправленных сообщений
    ValueTask<History.Response> GetHistory(HistoryOptions options);

    // Получение информации об операторе абонента
    ValueTask<Operator.Response> GetOperatorInfo(string phone);

    // Получение статистики
    ValueTask<Statistics.Response> GetStatistics(
        DateOnly? date = default,
        DateOnly? endDate = default,
        byte? currency = default,
        byte? balance = default);
}