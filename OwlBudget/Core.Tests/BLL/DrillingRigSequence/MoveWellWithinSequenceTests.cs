using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.MoveWellToSequence;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using NSubstitute;
using NUnit.Framework;

namespace Core.Tests.BLL.DrillingRigSequence;

public class MoveWellWithinSequenceTests
{
    private const int Amount = 8;
    private IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private MoveWellToSequenceHandler _handler;
    private Domain.DrillingRigSequence _sequence;
    private IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    [SetUp]
    public void Setup()
    {
        var sequenceId = Guid.NewGuid();
        _sequence = new Domain.DrillingRigSequence
        {
            Id = sequenceId,
            WellsToDrillingRigSequence = Enumerable.Range(1, Amount).Select(_ => new WellToDrillingRigSequence
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 11)),
                WellId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 10)),
                DrillingRigSequenceId = sequenceId,
                DrillingOrder = _
            }).ToList()
        };

        _wellsToDrillingRigSequencesRepository = Substitute.For<IWellsToDrillingRigSequencesRepository>();
        _wellsToDrillingRigSequencesRepository
            .GetByWellAndSequenceAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x =>
            {
                return _sequence.WellsToDrillingRigSequence.Single(_ => _.WellId == (Guid) x[0]);
            });
        _wellsToDrillingRigSequencesRepository.GetByScequenceIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(_sequence.WellsToDrillingRigSequence);
        _drillingRigSequenceRepository = Substitute.For<IDrillingRigSequenceRepository>();
        _handler = new MoveWellToSequenceHandler(_wellsToDrillingRigSequencesRepository,
            _drillingRigSequenceRepository);
    }

    [TestCase(2, 50, new byte[] {10, 50, 20, 30, 40, 60, 70, 80})]
    [TestCase(5, 10, new byte[] {20, 30, 40, 50, 10, 60, 70, 80})]
    [TestCase(1, 50, new byte[] {50, 10, 20, 30, 40, 60, 70, 80})]
    [TestCase(5, 80, new byte[] {10, 20, 30, 40, 80, 50, 60, 70})]
    [TestCase(8, 50, new byte[] {10, 20, 30, 40, 60, 70, 80, 50})]
    [TestCase(8, 10, new byte[] {20, 30, 40, 50, 60, 70, 80, 10})]
    [TestCase(1, 80, new byte[] {80, 10, 20, 30, 40, 50, 60, 70})]
    public async Task MoveWithinSequenceTest(int destinationOrder, byte wellId, byte[] wellIds)
    {
        var request = new MoveWellToSequenceRequest(_sequence.Id, _sequence.Id,
            new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, wellId), destinationOrder);

        await _handler.HandleIt(request, CancellationToken.None);
        Assert.IsTrue(Enumerable.Range(1, Amount)
            .Any(_ => _sequence.WellsToDrillingRigSequence.Any(w => w.DrillingOrder == _)));
        for (var i = 1; i <= Amount; i++)
        {
            var item = _sequence.WellsToDrillingRigSequence.Single(_ => _.DrillingOrder == i);
            var expectedId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, wellIds[i - 1]);
            Assert.AreEqual(expectedId, item.WellId);
        }
    }
}