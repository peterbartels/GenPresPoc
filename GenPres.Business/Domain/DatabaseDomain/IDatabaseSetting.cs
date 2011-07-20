using System;

namespace GenPres.Business.Domain.DatabaseDomain
{
    public interface IDatabaseSetting
    {
        String Name { get; set; }
        String GenPresConnectionString { get; set; }
        String PdmsConnectionString { get; set; }
        String GenFormWebservice { get; set; }
        String Machine { get; set; }
    }
}