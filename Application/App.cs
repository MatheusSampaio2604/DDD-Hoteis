using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{

    public class App<TViewModel, TModel> : IApp<TViewModel, TModel>
     where TViewModel : class
     where TModel : class
    {
        protected readonly ILogger<App<TViewModel, TModel>> _logger;
        protected readonly IMapper _mapper;
        protected readonly IRepository<TModel> _repository;

        public App(ILogger<App<TViewModel, TModel>> logger, IMapper mapper, IRepository<TModel> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<TModel> CreateAsync(TViewModel viewModel)

        {

            try

            {

                var map = _mapper.Map<TViewModel, TModel>(viewModel);

                await _repository.CreateAsync(map);

                return map;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex.Message);

                return null;

            }

        }


        public virtual async Task<TViewModel> EditAsync(TViewModel entity)
        {
            try
            {
                var map = _mapper.Map<TViewModel, TModel>(entity);

                await _repository.EditAsync(map);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public virtual async Task<TViewModel> FindOneAsync(int id)
        {
            TModel map = await _repository.FindOneAsync(id);

            TViewModel mapper = _mapper.Map<TModel, TViewModel>(map);

            return mapper;
        }


        public virtual async Task<TViewModel> FindNoTrackinOneAsync(int id)
        {
            TModel map = await _repository.FindNoTrackinOneAsync(id);

            TViewModel mapper = _mapper.Map<TModel, TViewModel>(map);

            return mapper;

        }


        public virtual async Task<IEnumerable<TViewModel>> FindAllAsync()
        {
            try
            {
                IEnumerable<TModel> models = await _repository.FindAllAsync();

                IEnumerable<TViewModel> modelViews = _mapper.Map<IEnumerable<TModel>, IEnumerable<TViewModel>>(models);

                return modelViews;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new List<TViewModel>();
        }


        public async Task<int> Remove(TModel model)
        {
            return await _repository.Remove(model);
        }


    }

    public class App<TModel> : IApp<TModel> where TModel : class
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<App<TModel>> _logger;
        protected readonly IRepository<TModel> _repository;

        public App(ILogger<App<TModel>> logger, IMapper mapper, IRepository<TModel> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }


        public virtual async Task<TModel> CreateAsync(TModel viewModel)
        {
            try
            {
                await _repository.CreateAsync(viewModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }


        public virtual async Task<TModel> EditAsync(TModel viewModel)
        {
            try
            {
                await _repository.EditAsync(viewModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public virtual async Task<TModel> FindOneAsync(int id)
        {
            try
            {
                return await _repository.FindOneAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }


        public virtual async Task<TModel> FindNoTrackinOneAsync(int id)
        {
            try
            {
                return await _repository.FindNoTrackinOneAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return default;
        }


        public virtual async Task<IEnumerable<TModel>> FindAllAsync()
        {
            try
            {
                return await _repository.FindAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new List<TModel>();
        }


        public async Task<int> Remove(TModel model)
        {
            return await _repository.Remove(model);
        }


    }
}
