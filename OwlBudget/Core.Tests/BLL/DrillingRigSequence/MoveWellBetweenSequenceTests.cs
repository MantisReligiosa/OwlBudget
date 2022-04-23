using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.MoveWellToSequence;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using NSubstitute;
using NUnit.Framework;

namespace Core.Tests.BLL.DrillingRigSequence;

public class MoveWellBetweenSequenceTests
{
    private const int Amount = 8;
    private IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private MoveWellToSequenceHandler _handler;
    private Domain.DrillingRigSequence _sequence1;
    private Domain.DrillingRigSequence _sequence2;
    private List<WellToDrillingRigSequence> _wells;
    private IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    [SetUp]
    public void Setup()
    {
        var sequence1Id = Guid.NewGuid();
        var sequence2Id = Guid.NewGuid();

        _wells = Enumerable.Range(1, Amount).Select(_ => new WellToDrillingRigSequence
        {
            Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 10)),
            WellId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 10)),
            DrillingRigSequenceId = sequence1Id,
            DrillingOrder = _
        }).Union(Enumerable.Range(1, Amount).Select(_ => new WellToDrillingRigSequence
        {
            Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 11)),
            WellId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte) (_ * 11)),
            DrillingRigSequenceId = sequence2Id,
            DrillingOrder = _
        })).ToList();

        _sequence1 = new Domain.DrillingRigSequence
        {
            Id = sequence1Id,
            WellsToDrillingRigSequence = _wells.Where(_ => _.DrillingRigSequenceId == sequence1Id).ToList()
        };


        _sequence2 = new Domain.DrillingRigSequence
        {
            Id = sequence2Id,
            WellsToDrillingRigSequence = _wells.Where(_ => _.DrillingRigSequenceId == sequence2Id).ToList()
        };

        _wellsToDrillingRigSequencesRepository = Substitute.For<IWellsToDrillingRigSequencesRepository>();
        _wellsToDrillingRigSequencesRepository
            .GetByWellAndSequenceAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x =>
            {
                return _wells.Single(_ => _.WellId == (Guid) x[0] && _.DrillingRigSequenceId == (Guid) x[1]);
            });
        _wellsToDrillingRigSequencesRepository.GetByScequenceIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(x => { return _wells.Where(_ => _.DrillingRigSequenceId == (Guid) x[0]).ToList(); });

        _drillingRigSequenceRepository = Substitute.For<IDrillingRigSequenceRepository>();
        _handler = new MoveWellToSequenceHandler(_wellsToDrillingRigSequencesRepository,
            _drillingRigSequenceRepository);
    }

    [TestCase(2, 50, new byte[] {10, 20, 30, 40, 60, 70, 80}, new byte[] {11, 50, 22, 33, 44, 55, 66, 77, 88})]
    [TestCase(5, 10, new byte[] {20, 30, 40, 50, 60, 70, 80}, new byte[] {11, 22, 33, 44, 10, 55, 66, 77, 88})]
    [TestCase(1, 50, new byte[] {10, 20, 30, 40, 60, 70, 80}, new byte[] {50, 11, 22, 33, 44, 55, 66, 77, 88})]
    [TestCase(5, 80, new byte[] {10, 20, 30, 40, 50, 60, 70}, new byte[] {11, 22, 33, 44, 80, 55, 66, 77, 88})]
    [TestCase(8, 50, new byte[] {10, 20, 30, 40, 60, 70, 80}, new byte[] {11, 22, 33, 44, 55, 66, 77, 50, 88})]
    [TestCase(8, 10, new byte[] {20, 30, 40, 50, 60, 70, 80}, new byte[] {11, 22, 33, 44, 55, 66, 77, 10, 88})]
    [TestCase(1, 80, new byte[] {10, 20, 30, 40, 50, 60, 70}, new byte[] {80, 11, 22, 33, 44, 55, 66, 77, 88})]
    public async Task MoveBetweenSequenceTest(int destinationOrder, byte wellId, byte[] wellIds1, byte[] wellIds2)
    {
        var request = new MoveWellToSequenceRequest(_sequence1.Id, _sequence2.Id,
            new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, wellId), destinationOrder);
        await _handler.HandleIt(request, CancellationToken.None);

        CheckSequence(_sequence1, wellIds1);
        CheckSequence(_sequence2, wellIds2);
    }

    private void CheckSequence(Domain.DrillingRigSequence sequence, byte[] wellIds)
    {
        var items = _wells.Where(_ => _.DrillingRigSequenceId == sequence.Id).ToArray();
        Assert.IsTrue(Enumerable.Range(1, items.Length).Any(_ => items.Any(w => w.DrillingOrder == _)));
        for (var i = 1; i <= items.Length; i++)
        {
            var item = items.Single(_ => _.DrillingOrder == i);
            var expectedId = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, wellIds[i - 1]);
            Assert.AreEqual(expectedId, item.WellId);
        }
    }
}