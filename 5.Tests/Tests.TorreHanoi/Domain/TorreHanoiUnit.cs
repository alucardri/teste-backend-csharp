using System;
using Domain.TorreHanoi;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Id);

            Assert.IsNotNull(torre.Discos);
            Assert.AreEqual(torre.Discos.Count, 3);

            Assert.IsNotNull(torre.Destino);
            Assert.AreEqual(torre.Destino.Discos.Count, 0);

            Assert.IsNotNull(torre.Intermediario);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);

            Assert.IsNotNull(torre.Origem);
            Assert.AreEqual(torre.Origem.Discos.Count, 3);

            Assert.IsTrue(torre.DataCriacao > DateTime.Today);
            Assert.AreEqual(torre.Status, TipoStatus.Pendente);
            Assert.IsNotNull(torre.PassoAPasso);


            Assert.AreEqual(torre.Origem.Tipo, TipoPino.Origem);
            Assert.AreEqual(torre.Destino.Tipo, TipoPino.Destino);
            Assert.AreEqual(torre.Intermediario.Tipo, TipoPino.Intermediario);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deve_Retornar_Sucesso()
        {
            var quantidadeDiscos = 3;
            var quantidadePassos = Math.Pow(2, quantidadeDiscos) - 1;

            var torre = new global::Domain.TorreHanoi.TorreHanoi(quantidadeDiscos, _mockLogger.Object);

            torre.Processar();

            Assert.AreEqual(TipoStatus.FinalizadoSucesso, torre.Status);
            Assert.AreEqual(quantidadeDiscos, torre.Destino.Discos.Count);
            Assert.AreEqual(0, torre.Origem.Discos.Count);
            Assert.AreEqual(0, torre.Intermediario.Discos.Count);
            Assert.AreEqual(quantidadePassos, torre.PassoAPasso.Count);
        }
    }
}
