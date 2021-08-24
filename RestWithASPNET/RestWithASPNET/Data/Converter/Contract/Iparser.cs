using System.Collections.Generic;

namespace RestWithASPNET.Data.Converter.Contract{
    public interface Iparser<origin, destiny>{ //tipos genericos destino e origem
        destiny parse(origin o);
        List<destiny> parse(List<origin> o);
    }
}